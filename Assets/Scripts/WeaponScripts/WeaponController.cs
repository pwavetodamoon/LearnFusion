using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject _container;
    public bool handEmpty = true;
    public GameObject weapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && handEmpty)
        {
            Debug.Log("touch");
            other.transform.SetParent(_container.transform);
            other.transform.position = _container.transform.position;
            other.transform.rotation = _container.transform.rotation;
            handEmpty = false;
        }
    }
}
