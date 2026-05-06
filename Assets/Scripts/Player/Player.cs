using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMode
{
    Normal,
    Hunting,
    Jumping,
}

public class Player : MonoBehaviour
{
  private const float GROUNDED_CHECK = 0.3f;
    [BoxGroup("Movement Parameters")] [SerializeField] private Transform groundedCheck;
    [BoxGroup("Movement Parameters")] [SerializeField] private LayerMask groundedLayer;
    [BoxGroup("Movement Parameters")] [SerializeField] private float speed = 5.0f;
    [BoxGroup("Movement Parameters")] [SerializeField] private float fov = 60f;
    [BoxGroup("Movement Parameters")] [SerializeField] private float jumpOffset = 0.2f;
    [BoxGroup("Hunting Parameters")] [SerializeField] private float huntingSpeed = 3.0f;
    [BoxGroup("Hunting Parameters")] [SerializeField] private float huntingFov = 100f;

    // Components
    private Rigidbody _rb;
    private CameraController _camera;
    private JumpDetector _jumpDetector;
    private Interactor _interactor;

    // Private Stuff
    private Inputs _inputs;
    private Vector2 _movement;
    private PlayerMode _mode = PlayerMode.Normal;
    private float _jumpTimer;
    private bool _grounded;
    private readonly RaycastHit[] raycastHits = new RaycastHit[1];

    // Public Stuff
    public Vector3 Velocity => _rb.linearVelocity;
    
    private void Awake()
    {
        _inputs = new Inputs();
        _rb = GetComponent<Rigidbody>();
        _jumpDetector = GetComponentInChildren<JumpDetector>();
        _interactor = GetComponentInChildren<Interactor>();
    }

    public void InitCamera(CameraController cameraController)
    {
        _camera = cameraController;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _inputs.Enable();
        _inputs.Player.Move.performed += MovePerformed;
        _inputs.Player.Move.canceled += MoveCancelled;
        _inputs.Player.Hunt.performed += HuntPerformed;
        _inputs.Player.Hunt.canceled += HuntCancelled;
        _inputs.Player.Jump.performed += JumpPerformed;
        _inputs.Player.Interact.performed += InteractPerformed;
    }

    private void FixedUpdate()
    {
      CheckGrounded();
        // Move normally for both
        switch (_mode)
        {
            case PlayerMode.Normal:
                Moving();
                break;
            case PlayerMode.Hunting:
                Hunting();
                break;
            case PlayerMode.Jumping:
                Jumping();
                break;
        }
    }

    private void CheckGrounded()
    {
      int hits = Physics.RaycastNonAlloc(groundedCheck.position, groundedCheck.TransformDirection(Vector3.down), raycastHits, GROUNDED_CHECK, groundedLayer);
      _grounded = hits > 0;
    }

    private void Moving()
    {
        Move(speed);
    }

    private void Hunting()
    {
        Move(huntingSpeed);
    }

    private void Move(float moveSpeed)
    {
      Vector3 forward = _camera.Camera.transform.forward;
      Vector3 right = _camera.Camera.transform.right;
      forward.y = 0;
      right.y = 0;
      forward.Normalize();
      right.Normalize();

      Vector3 targetDir = forward * _movement.y + right * _movement.x;
      Vector3 targetVelocity = targetDir * moveSpeed;
      Vector3 velocity = _rb.linearVelocity;
      velocity.x = targetVelocity.x;
      velocity.z = targetVelocity.z;
      _rb.linearVelocity = velocity;

      if (targetDir.sqrMagnitude > 0.001f) {
        Quaternion toRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        _rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime));
      }
    }

    private void MovePerformed(InputAction.CallbackContext obj)
    { _movement = obj.action.ReadValue<Vector2>();
    }
    
    private void MoveCancelled(InputAction.CallbackContext _)
    {
        _movement = Vector2.zero;
    }
    
    private void HuntPerformed(InputAction.CallbackContext obj)
    {
        _mode = PlayerMode.Hunting;
        _camera.AnimateFOV(huntingFov);
    }
    
    private void HuntCancelled(InputAction.CallbackContext _)
    {
        _mode = PlayerMode.Normal;
        _camera.AnimateFOV(fov);
    }
    
    private void JumpPerformed(InputAction.CallbackContext _)
    {
      if (_mode != PlayerMode.Normal) return;
      if (!_grounded) return;
      if (!_jumpDetector.TryGetClosest(out JumpTarget target)) return;
      Jump(target.transform.position);
    }

    private void Jump(Vector3 target)
    {
      // Source: https://en.wikipedia.org/wiki/Projectile_motion#Angle_%CE%B8_required_to_hit_coordinate_(x,_y)
      // 1. Get the launch angle to allow for the lowest possible velocity
      Vector3 adjustedTarget = new(target.x, target.y + jumpOffset, target.z);
      Vector3 difference = adjustedTarget - transform.position;
      Vector2 xzDiff = new(difference.x, difference.z);
      Vector2 xzDir = xzDiff.normalized;
      float xz = xzDiff.magnitude;
      float y = difference.y;
      float a = Mathf.Atan2(y, xz);
      float angle = (Mathf.PI / 2 + a) / 2;

      // 2. Get the initial speed
      float numerator = -Physics.gravity.y * xz * xz;
      float denominator = 2 * Mathf.Cos(angle) * Mathf.Cos(angle) * (xz * Mathf.Tan(angle) - y);
      float speed = Mathf.Sqrt(numerator / denominator);

      // 3. Calculate the time and set variables
      float speedX = speed * Mathf.Cos(angle);
      float speedY = speed * Mathf.Sin(angle);
      float time = xz / speedX;

      // 4. Jump and face the jump direction!
      Vector3 initialVelocity = new(xzDir.x * speedX, speedY, xzDir.y * speedX);
      _rb.linearVelocity = initialVelocity;
      _jumpTimer = time;

      _mode = PlayerMode.Jumping;
    }

    private void Jumping()
    {
      Vector3 jumpDirection = new(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
      Quaternion toRotation = Quaternion.LookRotation(jumpDirection.normalized);
      _rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime));
      _jumpTimer -= Time.fixedDeltaTime;
      if (_jumpTimer < 0f) {
        _mode = PlayerMode.Normal;
      }
    }

    private void InteractPerformed(InputAction.CallbackContext _)
    {
      if (_mode != PlayerMode.Normal) return;
      if(!_interactor.TryGetClosest(out Interactable interactable)) return;
      interactable.Activate(_interactor);
    }
}
