using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NetworkPlayerMovementHandle : NetworkBehaviour
{
    private NetworkPlayerControllerBase _networkPlayerControllerBase;

    private void Awake()
    {
        _networkPlayerControllerBase = GetComponent<NetworkPlayerControllerBase>();
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
    }
}