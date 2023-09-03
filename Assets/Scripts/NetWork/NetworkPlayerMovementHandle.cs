using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NetworkPlayerMovementHandle : NetworkBehaviour
{
    private NetworkPlayerControllerBase _networkPlayerControllerBase;
    public Animator _animator;

    private void Awake()
    {
        _networkPlayerControllerBase = GetComponent<NetworkPlayerControllerBase>();
        _animator.GetComponent<Animator>();
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        if (GetInput(out NetworkInputData data))
        {
            data.InputData.Normalize();
            _networkPlayerControllerBase.Move(data.InputData);

            if (data.JumpIsPressed == true)
            {
                _networkPlayerControllerBase.Jump();
            }
        }
        // Vector2 WalkSpeed = new Vector2(_networkPlayerControllerBase._VelocityDefault.x, _networkPlayerControllerBase._VelocityDefault.z);
        // WalkSpeed.Normalize();
        // float valueWalkSpeed = Mathf.Clamp01(WalkSpeed.sqrMagnitude);
        // _animator.SetFloat("Move", valueWalkSpeed);
    }
}