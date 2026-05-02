using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerMode
{
    Normal,
    Hunting,
}

public class Player : MonoBehaviour
{
    [BoxGroup("Movement Parameters")] [SerializeField] private float speed = 5.0f;
    [BoxGroup("Movement Parameters")] [SerializeField] private float fov = 60f;
    [BoxGroup("Movement Parameters")] [SerializeField] private float jumpSpeed = 5.0f;
    [BoxGroup("Hunting Parameters")] [SerializeField] private float huntingSpeed = 3.0f;
    [BoxGroup("Hunting Parameters")] [SerializeField] private float huntingFov = 100f;

    // Components
    private Rigidbody _rb;
    private CameraController _camera;
    private JumpDetector _jumpDetector;

    // Private Stuff
    private Inputs _inputs;
    private Vector2 _movement;
    private PlayerMode _mode = PlayerMode.Normal;
    
    private void Awake()
    {
        _inputs = new Inputs();
        _rb = GetComponent<Rigidbody>();
        _jumpDetector = GetComponentInChildren<JumpDetector>();
    }

    public void InitCamera(CameraController cameraController)
    {
        _camera = cameraController;
    }

    private void Start()
    {
        _inputs.Enable();
        _inputs.Player.Move.performed += MovePerformed;
        _inputs.Player.Move.canceled += MoveCancelled;
        _inputs.Player.Hunt.performed += HuntPerformed;
        _inputs.Player.Hunt.canceled += HuntCancelled;
        _inputs.Player.Jump.performed += JumpPerformed;
    }

    private void FixedUpdate()
    {
        // Move normally for both
        switch (_mode)
        {
            case PlayerMode.Normal:
                NormalMode();
                break;
            case PlayerMode.Hunting:
                HuntingMode();
                break;
        }
    }

    private void NormalMode()
    {
        Move(speed);
    }

    private void HuntingMode()
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

      Vector3 moveDirection = forward * _movement.y + right * _movement.x;
      _rb.linearVelocity = moveDirection * moveSpeed;

      if (moveDirection.sqrMagnitude > 0.001f) {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        _rb.MoveRotation(Quaternion.Slerp(transform.rotation, toRotation, 10f * Time.fixedDeltaTime));
      }
    }

    private void MovePerformed(InputAction.CallbackContext obj)
    {
        _movement = obj.action.ReadValue<Vector2>();
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
      if (!_jumpDetector.TryGetClosest(out JumpTarget target)) return;
      transform.position = target.transform.position;
    }
}
