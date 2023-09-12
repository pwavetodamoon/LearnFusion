using Fusion;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Axe", menuName = "ScriptableObject/Axe")]
public class AxeSO : ScriptableObject
{
    [SerializeField] private string _weaponName;
    [SerializeField] private GameObject _prefabsWeapon;
    [SerializeField] private int _weaponReliability = 10;
    [SerializeField] private NetworkBool _canPick = true;

    public void DeReliability(int amout)
    {
        _weaponReliability -= amout;
    }
    public void SetCanPick(bool value)
    {
        _canPick = value;
    }
    public bool GetCanPick()
    {
        return _canPick;
    }

    public void SetName(string value)
    {
        _weaponName = value;
    }
}
