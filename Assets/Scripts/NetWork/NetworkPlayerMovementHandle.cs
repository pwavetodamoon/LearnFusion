using Fusion;
using UnityEngine;
public class NetworkPlayerMovementHandle : NetworkBehaviour
{
    private NetworkPlayerControllerBase _networkPlayerControllerBase;
    private Animator _animator;
    private float MoveValueAnimation;
    private void Awake()
    {
        _networkPlayerControllerBase = GetComponent<NetworkPlayerControllerBase>();
        _animator = GetComponent<Animator>();
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        if (GetInput(out NetworkInputData data))
        {
            data.InputData.Normalize();
            SetAnimationMovement();
            _networkPlayerControllerBase.Move(data.InputData);
            if (data.JumpIsPressed == true)
            {
                _networkPlayerControllerBase.Jump();
                _animator.SetBool("isJump", true);
            }
            else
            {
                _animator.SetBool("isJump", false);
            }
        }
    }
    public virtual void SetAnimationMovement()
    {
        Vector2 WalkSpeed = new Vector2(_networkPlayerControllerBase._VelocityDefault.x, _networkPlayerControllerBase._VelocityDefault.z);
        WalkSpeed.Normalize();
        MoveValueAnimation = Mathf.Lerp(MoveValueAnimation, Mathf.Clamp01(WalkSpeed.sqrMagnitude), Runner.DeltaTime * 2.5f);
        _animator.SetFloat("Move", MoveValueAnimation);
    }

}