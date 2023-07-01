using UnityEngine;
using Player; // to give weapon to player


[RequireComponent(typeof(BoxCollider))]
public class Wallbuy : PlayerInteractable
{
    [SerializeField]
    private Weapon weaponToBuy;
    [SerializeField]
    private int cost;



    public void BuyWeapon()
    {
        GameObject player = PlayerRef.Instance.GetComponent<GameObject>();
        PlayerPoints points = player.GetComponent<PlayerPoints>();

        if (points.Purchase(cost))
        {
            //Give weapon to player
            PlayerInventory playerInventory = PlayerRef.Instance.GetComponentInChildren<PlayerInventory>();
            playerInventory.AddWeaponToList(weaponToBuy);
        }
    }
}
