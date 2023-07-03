using UnityEngine;
using Player; // to give weapon to player


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Wallbuy : PlayerInteractable
{
    [SerializeField]
    private WeaponStats weaponToBuy;
    [SerializeField]
    private int cost;

	private AudioSource audioSource;

	private void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

    public override void Interact()
    {
        Buy();
    }
    public void Buy()
	{
		if(base.CanInteract()){
			PlayerRef player = PlayerRef.Instance.GetComponent<PlayerRef>();
			PlayerPoints points = player.GetComponent<PlayerPoints>();
			if (points.Purchase(cost))
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
    
	private void Sound(){
		audioSource.Play();
	}
}
