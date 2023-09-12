using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;

public class weapon : NetworkBehaviour
{
    [SerializeField] protected AxeSO axeSO;

    public AxeSO _axeSO => axeSO;
    private void Start()
    {
        SetNAme();

    }
    private void SetNAme()
    {
        _axeSO.SetName(gameObject.name);
        Debug.Log("SetNAme");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _axeSO.SetCanPick(false);
            Debug.Log("OnTriggerStay: " + _axeSO.GetCanPick());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _axeSO.SetCanPick(true);
            Debug.Log("OnTriggerStay: " + _axeSO.GetCanPick());
        }
    }
}
