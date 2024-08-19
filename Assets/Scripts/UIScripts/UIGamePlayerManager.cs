using System.Collections;
using deVoid.UIFramework;
using deVoid.Utils;
using Fusion;
using UnityEngine;

public sealed class UIGamePlayManager : NetworkBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameGamePlay;
    public UIChat uiChat;
    private void Awake()
    {
        _uIFrameGamePlay = _defaultUISetting.CreateUIInstance();
        AddListeners();
    }
    private void Start()
    {
        ShowUIChat();
    }
    private void AddListeners()
    {
       // Signals.Get<ShowUIChat>().AddListener(ShowUIChat);
        Signals.Get<OpenChatBox>().AddListener(InputTextPlayerSelect);
        Signals.Get<SendPlayerMessage>().AddListener(SendPlayerMesseage);


    }

    private void RemoveAddListeners()
    {
      //  Signals.Get<ShowUIChat>().RemoveListener(ShowUIChat);
        Signals.Get<OpenChatBox>().RemoveListener(InputTextPlayerSelect);
        Signals.Get<SendPlayerMessage>().RemoveListener(SendPlayerMesseage);

    }
    private void OnDestroy()
    {
        RemoveAddListeners();
    }

    private void ShowUIMovement()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UIMovement);

    }
    private void ShowUIChat()
    {
        _uIFrameGamePlay.ShowPanel(ScreenIds.UIChat);
        StartCoroutine(LoadUIChat());

    }
    private void InputTextPlayerSelect()
    {
        uiChat.PlayerInputText.Select();
    }
    
    private void SendPlayerMesseage()
    {
                  var  chatManager = FindObjectOfType<ChatManager>();
                  if (chatManager == null)
                  {
                      Debug.Log("chat null");
                  }
                  // if (string.IsNullOrWhiteSpace(uiChat.PlayerInputText.text))
                  // {
                  //     Debug.Log("string null");
                  // }
                    if (chatManager != null && !string.IsNullOrWhiteSpace(uiChat.PlayerInputText.text))
                    {
                        chatManager.CallSendMessage(uiChat.PlayerInputText.text);
                        uiChat.PlayerInputText.text = string.Empty;
                    }
    }

    public IEnumerator LoadUIChat()
    {
        uiChat = FindObjectOfType<UIChat>();
        yield return new WaitForSeconds(5f);
    }
}