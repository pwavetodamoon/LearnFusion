using deVoid.UIFramework;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using deVoid.Utils;

public class UIMainMenuManager : APanelController
{
    public TMP_InputField Name_InputField;
    public Button _JoinButton;
    protected override void Awake()
    {
        base.Awake();
        AddListeners();
    }
    private void Start()
    {
        // nếu tên đã được đặt thì
        if (PlayerPrefs.HasKey("PlayerNickName"))
        {
            Name_InputField.text = PlayerPrefs.GetString("PlayerNickName");
        }
    }
    protected override void AddListeners()
    {
        base.AddListeners();
        _JoinButton.onClick.AddListener(OnFindButtonClick);
    }
    private void RemoveAddListeners()
    {
        _JoinButton.onClick.RemoveListener(OnFindButtonClick);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        RemoveAddListeners();
    }
    public void OnFindButtonClick()
    {
        PlayerPrefs.SetString("PlayerNickName", Name_InputField.text);
        PlayerPrefs.Save();
        NetworkRunnerHandle networkRunnerHandle = FindObjectOfType<NetworkRunnerHandle>();
        networkRunnerHandle.OnJoinLobby();
        Signals.Get<HideUIMainMenu>().Dispatch();
    }
}
