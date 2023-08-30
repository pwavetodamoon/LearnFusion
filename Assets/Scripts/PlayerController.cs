using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    private NewInputSystem _inputSystem;
    private CharacterController _characterController;
    private Animator _animator;

    private Vector2 _input;
    private Vector3 _moveDir;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 10.0f;

    [SerializeField] private float _smoothTime = 0.1f;
    [SerializeField] private float _jumpPower;

    private bool isGrounded() => _characterController.isGrounded;

    [Header("Gravity")]
    [SerializeField] private float _gravity = 1.0f;

    private float _gravityDefault = -9.81f;
    private float _velocity;

    private float _currentVelocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputSystem = new NewInputSystem();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
        _inputSystem.Player.Move.performed += MovePerformed;
        _inputSystem.Player.Move.canceled += MoveCanceled;
        _inputSystem.Player.Jump.performed += JumpPerformed;
        _inputSystem.Player.Jump.canceled += JumpCanceled;
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
        _inputSystem.Player.Move.performed -= MovePerformed;
        _inputSystem.Player.Move.canceled -= MoveCanceled;
        _inputSystem.Player.Jump.performed -= JumpPerformed;
        _inputSystem.Player.Jump.canceled -= JumpCanceled;
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _moveDir = new Vector3(_input.x, 0f, _input.y);
        _animator.SetBool("isRunning", true);
    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {
        _input = Vector2.zero;
        _moveDir = Vector3.zero;
        _animator.SetBool("isRunning", false);
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        if (!isGrounded()) return;
        _velocity += _jumpPower;
        _animator.SetBool("isJumping", true);
    }

    private void JumpCanceled(InputAction.CallbackContext context)
    {
        _animator.SetBool("isJumping", false);
    }

    private void Update()
    {
        ApplyGravity();
        Rotate();
        Move();
    }

    private void Move()
    {
        _characterController.Move(_moveDir * _moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (_input.sqrMagnitude == 0f) return;
        var targetAngle = Mathf.Atan2(_moveDir.x, _moveDir.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, _smoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void ApplyGravity()
    {
        if (isGrounded() && _velocity <= 0f)
        {
            _velocity = -1f;
        }
        else
        {
            _velocity += _gravityDefault * _gravity * Time.deltaTime;
        }

        _moveDir.y = _velocity;
    }
}