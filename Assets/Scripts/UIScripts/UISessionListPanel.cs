using System;
using deVoid.UIFramework;
using deVoid.Utils;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UISessionListPanel : APanelController
{
    public TextMeshProUGUI _statusText;
    public GameObject _uISesionItemPrefabs;
    public Button CreateNewSessionButton;
    public VerticalLayoutGroup _contentContainerSessionItem;

    protected override void Awake()
    {
        base.Awake();
        ClearListUiItem();
        AddListeners();
    }
    protected override void AddListeners()
    {
        base.AddListeners();
        CreateNewSessionButton.onClick.AddListener(CreateNewGameButtonOnClick);
    }
    private void RemoveAddListeners()
    {
        CreateNewSessionButton.onClick.RemoveListener(CreateNewGameButtonOnClick);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        RemoveAddListeners();
    }
    public void ClearListUiItem()
    {
        foreach (Transform child in _contentContainerSessionItem.transform)
        {
            Destroy(child.gameObject);
        }
        _statusText.gameObject.SetActive(false);
        Debug.Log("clear");
    }

    public void AddListUiItem(SessionInfo sessionInfo)
    {
        //khai bao class SessionListItem , doi tuong UI item 
        SessionListItem addsessionListItem = Instantiate(_uISesionItemPrefabs, _contentContainerSessionItem.transform).GetComponent<SessionListItem>();
        // Dat thong tin cua Room hien tai vao session item
        addsessionListItem.SetInfomationForSession(sessionInfo);

        // Kết nối sự kiện để nắm đc time nhấn nút join
        addsessionListItem.OnJoinSession += AddListUiItem_OnJoinSession;
    }

    private void AddListUiItem_OnJoinSession(SessionInfo info)
    {
        Debug.Log("Test");
    }

    public void OnNoSessionFound()
    {
        _statusText.text = "No Session Found";
        _statusText.gameObject.SetActive(true);
    }

    public void OnLookingForSession()
    {
        _statusText.text = "Looking for Game Session";
        _statusText.gameObject.SetActive(false);
    }

    public void CreateNewGameButtonOnClick()
    {
        // todo: hien panel tao phong
        OnLookingForSession();
        Signals.Get<ShowUICreateNewSession>().Dispatch();
    }
}
