using UnityEngine;
using TMPro;
using Player;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory inventory;
    [SerializeField]
    private PlayerWeaponShooter shooter;
    [SerializeField]
    private PlayerWeaponAmmo ammo;

    private TMP_Text ammoText;

    private void Awake()
    {
        ammoText = GetComponent<TMP_Text>();
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        shooter.GunFireEvent -= WeaponFire;
    }

    private void Start()
    {
        shooter = PlayerRef.Instance.GetComponentInChildren<PlayerWeaponShooter>();
        ammo = PlayerRef.Instance.GetComponentInChildren<PlayerWeaponAmmo>();
        inventory = PlayerRef.Instance.GetComponentInChildren<PlayerInventory>();

        shooter.GunFireEvent += WeaponFire;
    }

    private void LateUpdate()
    {
        int currMag = ammo.FindAmmoDataFromWeapon(inventory.EquippedWeapon).magCount;
        int currRes = ammo.FindAmmoDataFromWeapon(inventory.EquippedWeapon).reserveCount;
        SetAmmoText(currMag, currRes);
    }


    private void WeaponFire(WeaponStats weapon)
    {
        // Get the current mag and reserve on that weapon.
    }

    private void SetAmmoText(int mag, int reserve)
    {
        ammoText.text = $"{mag} / {reserve}";
     
    }
}
