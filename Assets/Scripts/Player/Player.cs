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
    [BoxGroup("Hunting Parameters")] [SerializeField] private float huntingSpeed = 3.0f;
    [BoxGroup("Movement Parameters")] [SerializeField] private float huntingFov = 100f;

    private Inputs _inputs;
    private Rigidbody _rb;
    private Vector2 _movement;
    private PlayerMode _mode = PlayerMode.Normal;
    private CameraController _camera;
    
    private void Awake()
    {
        _inputs = new Inputs();
        _rb = GetComponent<Rigidbody>();
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
        Move(speed * _movement);
        Rotate();
    }

    private void HuntingMode()
    {
        Move(huntingSpeed * _movement);
    }

    private void Rotate()
    {
        Debug.Log(_camera.Camera.transform.rotation);
    }

    private void Move(Vector2 velocity)
    {
        _rb.linearVelocity = new Vector3(velocity.x, 0.0f, velocity.y);
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
}
