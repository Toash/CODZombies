using UnityEngine;
using Player; // to give weapon to player


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Wallbuy : PlayerInteractable
{
    [SerializeField]
    private WeaponStats weaponToBuy;
    [SerializeField]
    private int weaponCost;
	[SerializeField]
	private int reserveCost = 400;


	private PlayerInventory playerInv;
	private PlayerWeaponAmmo playerAmmo;
	private AudioSource audioSource;

	private void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

    private void Start()
    {
		playerInv = PlayerRef.Instance.GetComponentInChildren<PlayerInventory>();
		playerAmmo = PlayerRef.Instance.GetComponentInChildren<PlayerWeaponAmmo>();
    }

    public override void Interact()
    {
		int currAmmoReserve = playerAmmo.FindAmmoDataFromWStats(weaponToBuy).reserveCount;
		int currMaxReserve = weaponToBuy.MaxReserveCount;

		if (!playerInv.HasWeapon(weaponToBuy))
		{
			BuyWeapon();

		}
		else if (currAmmoReserve < currMaxReserve) 
        {
			BuyAmmo();
		}
        else
        {
			Debug.Log("Player already has the weapon with max reserves.");
		}
    }
    public void BuyWeapon()
	{
		if(base.CanInteract()){

			PlayerRef player = PlayerRef.Instance.GetComponent<PlayerRef>();
			PlayerPoints points = player.GetComponent<PlayerPoints>();
			if (points.Purchase(weaponCost))
			{
				Debug.Log("Interact: Bought");
				//Give weapon to player
				PlayerInventory playerInventory = PlayerRef.Instance.GetComponentInChildren<PlayerInventory>();
				playerInventory.AddWeaponToList(weaponToBuy);
				Sound();
			}
			Debug.Log("Interact: No money");
			base.ResetTimer();
		}
	}

	/// <summary>
	/// Buy ammo reserve
	/// </summary>
	public void BuyAmmo()
    {
		PlayerRef player = PlayerRef.Instance.GetComponent<PlayerRef>();
		PlayerPoints points = player.GetComponent<PlayerPoints>();
		if (points.Purchase(reserveCost))
        {
			playerAmmo.FillReserves(weaponToBuy);
			Sound();
		}


	}
    
	private void Sound(){
		audioSource.Play();
	}
}
