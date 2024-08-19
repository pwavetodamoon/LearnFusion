using Fusion;
using TMPro;
using UnityEngine;

public class ChatManager : NetworkBehaviour
{
    public UIGamePlayManager uIGamePlayManager;
    public TMP_Text msgText;
    public NetworkPlayer networkPlayer;
    public GameObject MsgTextPrefabInstance;
    public GameObject container;
    private GameObject _msgText;
    public string playerName;



    private void Start()
    {
        if (HasStateAuthority)
        {
            _msgText = Instantiate(MsgTextPrefabInstance, container.transform);
            msgText = _msgText.GetComponent<TMP_Text>();
            uIGamePlayManager = FindObjectOfType<UIGamePlayManager>();
            networkPlayer = GetComponent<NetworkPlayer>();
            playerName = networkPlayer.NickName.ToString();
        }
    }
    
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_CallSendMessage(string msg)
    {
            RPC_SendMessageFunction(msg);
            Debug.Log("Message Send Requested");
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_SendMessageFunction(string message, RpcInfo rpcInfo = default)
    {
        // Chỉ máy có StateAuthority mới xử lý và phát tán tin nhắn
        // Nếu không có quyền trạng thái, RPC sẽ không được xử lý
        if (HasStateAuthority)
        {
            RPC_DisPlayMessage(message, rpcInfo.Source);
            Debug.Log("Message Processed and Broadcasted");
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_DisPlayMessage(string message, PlayerRef messageSender)
    {
            // Hiển thị tin nhắn trên giao diện người dùng
            string displayMessage = $"{playerName}: {message}\n";

            // Kiểm tra nếu msgText null, tạo một đối tượng mới
            if (msgText == null)
            {
                _msgText = Instantiate(MsgTextPrefabInstance, container.transform);
                msgText = _msgText.GetComponent<TMP_Text>();
            }

            // Thêm tin nhắn mới vào chuỗi văn bản
            msgText.text += displayMessage;
            Debug.Log("Displayed Message: " + displayMessage);

            // Tạo một phiên bản mới của msgText trong container
            Instantiate(msgText, uIGamePlayManager.uiChat.container.transform);
            Debug.Log("Instantiate success");

            // Reset input field sau khi tin nhắn đã được gửi
            uIGamePlayManager.uiChat.PlayerInputText.text = string.Empty;
    }
}
