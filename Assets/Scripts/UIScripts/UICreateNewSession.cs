using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using deVoid;
using deVoid.UIFramework;
using TMPro;
using UnityEngine.UI;

public class UICreateNewSession : APanelController
{
    public TMP_InputField sessionNameInputField;
    public Button CreateNewGameButton;
    protected override void Awake()
    {
        base.Awake();
        AddListeners();
    }
    protected override void AddListeners()
    {
        base.AddListeners();
        CreateNewGameButton.onClick.AddListener(OnCreateNewSessionOnClick);
    }
    private void RemoveAddListeners()
    {
        CreateNewGameButton.onClick.RemoveListener(OnCreateNewSessionOnClick);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        RemoveAddListeners();
    }
    public void OnCreateNewSessionOnClick()
    {
        NetworkRunnerHandle networkRunnerHandle = FindObjectOfType<NetworkRunnerHandle>();
        networkRunnerHandle.CreateGame(sessionNameInputField.text, "Map1");
    }
}
