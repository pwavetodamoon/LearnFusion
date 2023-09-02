using UnityEngine;
using Fusion;
using TMPro;
using Cinemachine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    // class đai diện cho người chơi network Object
    public static NetworkPlayer local { get; set; }

    public CinemachineFreeLook cam;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            local = this;
            cam.enabled = true;
            Debug.Log("Spawned local player");
        }
        else
        {
            cam.enabled = false;
            Debug.Log("Spawned remote player");
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
            Debug.Log("Despawn");
        }
    }
}