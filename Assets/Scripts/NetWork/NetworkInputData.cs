using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector3 InputData;
    public NetworkBool JumpIsPressed;
    public NetworkBool IsPickUp;
    public NetworkBool IsDrop;
    public NetworkBool IsAttack;




}