using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class WeaponUI : MonoBehaviour
	{
		private PlayerInventory playerInventory;
		private TMP_Text weaponText;

		void Awake()
		{
			playerInventory = FindObjectOfType<PlayerInventory>();
			weaponText = this.GetComponent<TMP_Text>();
		}


		private void Update()
		{
			weaponText.text = playerInventory.equippedWeapon.name;
		}
	}
}

