using UnityEngine;
using Fusion;
using TMPro;
using Cinemachine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    // class đai diện cho người chơi network Object
    public TextMeshProUGUI _nameText;
    [Networked(OnChanged = nameof(OnNickNameChange))]
    public NetworkString<_16> NickName { get; set; }
    public static NetworkPlayer local { get; set; }
    public CinemachineFreeLook cam;
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            local = this;
            cam.enabled = true;
            Debug.Log("Spawned local player");

            // sau khi spawned thì đặt tên cho người chơi thông qua nơi lưu trữ ,
            // ở đây sẽ dùng PlayerPrefs<bộ lưu trữ cục bộ> (vd: có thể dùng Firebase chẳng hạn).

            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickName"));
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
    static void OnNickNameChange(Changed<NetworkPlayer> changed)
    {
        changed.Behaviour.OnNickNameChange();
    }
    private void OnNickNameChange()
    {
        Debug.Log($"nickname change to {NickName} for player {gameObject.name}");
        _nameText.text = NickName.ToString();
    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log($"RPC SetNickNam {nickName}");
        this.NickName = nickName;
    }
}