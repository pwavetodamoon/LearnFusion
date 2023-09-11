using Fusion;
using UnityEngine;
public class NetworkPlayerMovementHandle : NetworkBehaviour
{
    private NetworkPlayerControllerBase _networkPlayerControllerBase;
    public WeaponController _weaponController;
    private Animator _animator;
    private float MoveValueAnimation;
    private void Awake()
    {
        _networkPlayerControllerBase = GetComponent<NetworkPlayerControllerBase>();
        _weaponController = GetComponent<WeaponController>();
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

            if (data.IsPickUp == true)
            {
                _weaponController.RPC_Pickup();
            }

            if (data.IsDrop == true)
            {
                _weaponController.RPC_Drop();
            }


            if (data.IsAttack == true)
            {
                Debug.Log("Attack");
                _animator.SetBool("isAttack", true);

            }
            else
            {
                Debug.Log("Cancel Attack");
                _animator.SetBool("isAttack", false);
            }
        }
        return;
    }
    public virtual void SetAnimationMovement()
    {
        Vector2 WalkSpeed = new Vector2(_networkPlayerControllerBase._VelocityDefault.x, _networkPlayerControllerBase._VelocityDefault.z);
        WalkSpeed.Normalize();
        MoveValueAnimation = Mathf.Lerp(MoveValueAnimation, Mathf.Clamp01(WalkSpeed.sqrMagnitude), Runner.DeltaTime * 2.5f);
        _animator.SetFloat("Move", MoveValueAnimation);
    }

}