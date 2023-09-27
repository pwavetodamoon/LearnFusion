using deVoid.UIFramework;
using deVoid.Utils;
using UnityEngine;

public sealed class UIMenuManager : MonoBehaviour
{
    [SerializeField] private UISettings _defaultUISetting = null;
    private UIFrame _uIFrameMenu;
    private void Awake()
    {
        _uIFrameMenu = _defaultUISetting.CreateUIInstance();
        Addlisterner();
    }
    private void Start()
    {
        ShowUIMainMenu();
    }
    private void OnDestroy()
    {
        RemoveListener();
    }
    private void Addlisterner()
    {
        Signals.Get<ShowUISessionListPanel>().AddListener(ShowUISessionListPanel);
        Signals.Get<HideUIMainMenu>().AddListener(HideUIMainMenu);

        Signals.Get<ShowUICreateNewSession>().AddListener(ShowUICreateNewSession);
    }

    private void RemoveListener()
    {
        Signals.Get<ShowUISessionListPanel>().RemoveListener(ShowUISessionListPanel);
        Signals.Get<HideUIMainMenu>().RemoveListener(HideUIMainMenu);

        Signals.Get<ShowUICreateNewSession>().RemoveListener(ShowUICreateNewSession);
    }
    private void ShowUIMainMenu()
    {
        _uIFrameMenu.ShowPanel(ScreenIds.UIMainMenu);
    }
    private void HideUIMainMenu()
    {
        _uIFrameMenu.HidePanel(ScreenIds.UIMainMenu);
    }

    // UI SessionListPanel
    private void ShowUISessionListPanel()
    {
        _uIFrameMenu.ShowPanel(ScreenIds.UISessionListPanel);
    }
    private void HideUISessionListPanel()
    {
        _uIFrameMenu.HidePanel(ScreenIds.UISessionListPanel);
    }
    //

    //UICreateNewSession
    private void ShowUICreateNewSession()
    {
        _uIFrameMenu.ShowPanel(ScreenIds.UICreateNewSession);
    }
    private void HideUICreateNewSession()
    {
        _uIFrameMenu.HidePanel(ScreenIds.UICreateNewSession);
    }
}