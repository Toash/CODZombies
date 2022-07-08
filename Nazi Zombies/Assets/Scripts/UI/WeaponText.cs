using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class WeaponText : MonoBehaviour
	{
		private PlayerInventory playerInventory;

		void Awake()
		{
			playerInventory = FindObjectOfType<PlayerInventory>();
		}
		[SerializeField]
		private TMP_Text weaponText;

		private void Update()
		{
			weaponText.text = playerInventory.equippedWeapon.name;
		}
	}
}

