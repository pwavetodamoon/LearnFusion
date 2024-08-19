using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextChatController : MonoBehaviour
{
 public GameObject TextChatPrefabs;
 public GameObject container;
 public GameObject instantiatedChat;


 public UIGamePlayManager UIGamePlayManager;
 public TextMeshProUGUI TextChat;

 private void Start()
 {
     // Instantiate the prefab as a child of the container's transform
      instantiatedChat = Instantiate(TextChatPrefabs, container.transform);
     // Get the TextMeshProUGUI component from the instantiated prefab
     TextChat = instantiatedChat.GetComponentInChildren<TextMeshProUGUI>();
     StartCoroutine(UIGamePlayManager.LoadUIChat());
 }

 // private void OnEnable()
 // {
 //     instantiatedChat = Instantiate(TextChatPrefabs, container.transform);
 //     TextChat = instantiatedChat.GetComponentInChildren<TextMeshProUGUI>();
 // }
}
