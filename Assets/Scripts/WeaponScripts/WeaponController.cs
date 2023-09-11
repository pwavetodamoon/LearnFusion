using UnityEngine;
using Fusion;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;

public class WeaponController : NetworkBehaviour
{
    public GameObject _containerRightHand;
    public GameObject Weapon;
    public NetworkBool handEmpty = true;
    public float pickupRange = 1f; // Phạm vi xung quanh để phát hiện vũ khí

    void Update()
    {
        Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y * 3, this.transform.position.z), transform.forward, Color.green);
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public virtual void RPC_Pickup()
    {
        RaycastHit hit;
        // Tạo một ray bắt đầu từ vị trí của người chơi và theo hướng trước của họ
        Ray ray = new Ray(new Vector3(this.transform.position.x, this.transform.position.y * 3, this.transform.position.z), transform.forward);

        // Kiểm tra xem ray có va chạm với bất kỳ đối tượng nào trong phạm vi pickupRange không
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            Weapon = hit.collider.gameObject;
            if (hit.collider.CompareTag("Weapon") && handEmpty && Weapon.GetComponent<weapon>().CanPick)
            {
                Debug.Log("Picked up weapon");
                Weapon.transform.SetParent(_containerRightHand.transform, true);
                Weapon.transform.position = _containerRightHand.transform.position;
                Weapon.transform.rotation = _containerRightHand.transform.rotation;
            }
        }
        else
        {
            Debug.Log("Can not pick up weapon");

        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public virtual void RPC_Drop()
    {
        if (!handEmpty && Weapon != null && !Weapon.GetComponent<weapon>().CanPick)
        {
            Debug.Log("Drop");
            Weapon.transform.SetParent(null);
            handEmpty = true;
            Weapon = null;
        }
    }
}
