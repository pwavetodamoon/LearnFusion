using System;
using deVoid.UIFramework;
using deVoid.Utils;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UISessionListPanel : APanelController
{
    public TextMeshProUGUI _statusText;
    public GameObject _uISesionItemPrefabs;

    public VerticalLayoutGroup _contentContainerSessionItem;

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
        //khai bao class SessionListItem 
        SessionListItem addsessionListItem = Instantiate(_uISesionItemPrefabs, _contentContainerSessionItem.transform).GetComponent<SessionListItem>();
        // Dat thong tin cua Room hien tai vao session item
        addsessionListItem.SetInfomationForSession(sessionInfo);

        // Kết nối sự kiện để nắm đc time nhấn nút join
        addsessionListItem.OnJoinSession += AddListUiItem_OnJoinSession;
    }

    private void AddListUiItem_OnJoinSession(SessionInfo info)
    {

    }

    private void OnNoSessionFound()
    {
        _statusText.text = " No Session Found";
        _statusText.gameObject.SetActive(true);
    }

    private void OnLookingForSession()
    {
        _statusText.text = "Looking for Game Session";
        _statusText.gameObject.SetActive(false);
    }
}
