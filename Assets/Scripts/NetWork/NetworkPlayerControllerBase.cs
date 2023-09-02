using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[OrderBefore(typeof(NetworkTransform))]
[DisallowMultipleComponent]
public class NetworkPlayerControllerBase : NetworkTransform
{
    private CharacterController _characterController { get; set; }
    [Networked]
    public Vector3 _VelocityDefault { get; set; }

    [Networked]
    [HideInInspector]
    public bool isGrounded { get; set; }

    [Header("Movement")]
    [SerializeField] private float _smoothTime = 0.1f;
    [SerializeField] private float _jumpPower = 8.0f;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float braking = 10.0f;
    [SerializeField] private float maxSpeed = 2.0f;


    [Header("Gravity")]
    [SerializeField] private float _gravity = -20.0f;
    protected override void Awake()
    {
        base.Awake();
        if (_characterController == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }

    protected override void CopyFromBufferToEngine()
    {
        _characterController.enabled = false;

        // Pull base (NetworkTransform) state from networked data buffer
        base.CopyFromBufferToEngine();

        // Re-enable CC
        _characterController.enabled = true;
    }

    public virtual void Jump()
    {
        if (isGrounded)
        {
            //  Debug.Log("jump");
            var newVel = _VelocityDefault;
            newVel.y += _jumpPower;
            _VelocityDefault = newVel;
        }
    }
    public virtual void Move(Vector3 MoveDir)
    {
        var Velocity = _VelocityDefault;
        var previousPos = transform.position;
        // Debug.Log($"previousPos: {previousPos}");

        // áp dụng trọng lực
        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = 0f;
        }
        else
        {
            Velocity.y += _gravity * Runner.DeltaTime;
        }

        // Tạo một vector horizontalVel để lưu trữ vận tốc theo trục x và z
        var horizontalVel = default(Vector3);
        horizontalVel.x = Velocity.x;
        horizontalVel.z = Velocity.z;

        // Nếu không có tham số truyền vào từ CollectInputData
        if (MoveDir == default)
        {
            horizontalVel = Vector3.Lerp(horizontalVel, default, braking * Runner.DeltaTime);
        }
        else
        {
            // Cập nhật vận tốc ngang sử dụng hướng di chuyển và gia tốc
            horizontalVel = Vector3.ClampMagnitude(horizontalVel + MoveDir * acceleration * Runner.DeltaTime, maxSpeed);

            var targetAngle = Mathf.Atan2(horizontalVel.x, horizontalVel.z) * Mathf.Rad2Deg;
            var LerpAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, _smoothTime);
            transform.rotation = Quaternion.Euler(0, LerpAngle, 0);
        }
        // Cập nhật thành phần x và z của vận tốc tổng thể
        Velocity.x = horizontalVel.x;
        Velocity.z = horizontalVel.z;

        _characterController.Move(Velocity * Runner.DeltaTime);
        isGrounded = _characterController.isGrounded;
        _VelocityDefault = (transform.position - previousPos) * Runner.Simulation.Config.TickRate;
        //   Debug.Log($"_VelocityDefault {_VelocityDefault}");

    }

}