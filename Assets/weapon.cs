using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

public class weapon : NetworkBehaviour
{
    public NetworkBool CanPick = true;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanPick = false;
            Debug.Log("OnTriggerStay: " + CanPick);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanPick = true;
            Debug.Log("OnTriggerStay: " + CanPick);
        }
    }
}
