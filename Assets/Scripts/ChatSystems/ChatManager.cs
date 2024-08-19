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

    private void Start()
    {
        if (Object.HasStateAuthority)
        {
            _msgText = Instantiate(MsgTextPrefabInstance, container.transform);
            msgText = _msgText.GetComponent<TMP_Text>();
            uIGamePlayManager = FindObjectOfType<UIGamePlayManager>();
            networkPlayer = GetComponent<NetworkPlayer>();
        }
    }

    public void CallSendMessage(string msg)
    {
        // Gọi RPC để gửi tin nhắn đến tất cả các máy khách
        // Tất cả các máy có thể gọi phương thức này
        RPC_SendMessageFunction(msg);
        Debug.Log("Message Send Requested");
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_SendMessageFunction(string message, RpcInfo rpcInfo = default)
    {
        // Chỉ máy có StateAuthority mới xử lý và phát tán tin nhắn
        // Nếu không có quyền trạng thái, RPC sẽ không được xử lý
        if (Object.HasStateAuthority)
        {
            RPC_DisPlayMessage(message, rpcInfo.Source);
            Debug.Log("Message Processed and Broadcasted");
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_DisPlayMessage(string message, PlayerRef messageSender)
    {
        // Hiển thị tin nhắn trên giao diện người dùng
        if (Object.HasStateAuthority)
        {
            string playerName = networkPlayer.NickName.ToString();
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

            // Reset input field sau khi tin nhắn đã được gửi
            uIGamePlayManager.uiChat.PlayerInputText.text = string.Empty;
        }
    }
}
