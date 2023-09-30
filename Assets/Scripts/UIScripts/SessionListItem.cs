using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;
using System;
using UnityEngine.Assertions.Comparers;

public class SessionListItem : MonoBehaviour
{
    public TextMeshProUGUI _sessionNameText;
    public TextMeshProUGUI _playerCountText;
    public Button _joinButton;
    // chua tat ca thong tin cua mot session vi du nhu : ten session , playerCount
    SessionInfo sessionInfo;
    public event Action<SessionInfo> OnJoinSession; // <== kiểu trả về là một sự kiện 

    // Đặt thông tin cho mỗi room
    public void SetInfomationForSession(SessionInfo SessionInfo)
    {
        this.sessionInfo = SessionInfo;
        _sessionNameText.text = SessionInfo.Name;
        _playerCountText.text = $"{SessionInfo.PlayerCount.ToString()}/{SessionInfo.MaxPlayers.ToString()}";

        // them biến bool để kiểm tra phòng đầy hay chưa
        bool isJoinButtonActive = true;
        if (SessionInfo.PlayerCount >= SessionInfo.MaxPlayers)
        {
            isJoinButtonActive = false;
        }
        // đặt lại trạng thái cho button
        _joinButton.gameObject.SetActive(isJoinButtonActive);
    }
    public void Onclick()
    {
        NetworkRunnerHandle networkRunnerHandle = FindObjectOfType<NetworkRunnerHandle>();
        OnJoinSession?.Invoke(sessionInfo);
        networkRunnerHandle.JoinGame(sessionInfo);
    }
}
