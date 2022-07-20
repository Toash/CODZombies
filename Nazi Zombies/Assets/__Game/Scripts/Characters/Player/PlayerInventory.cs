using UnityEngine;
using System.Collections.Generic;


namespace Player
{
	public class PlayerInventory : MonoBehaviour
	{
		[HideInInspector]public Weapon equippedWeapon;
		[SerializeField]
		private PlayerInfo info;

		[Header("Inventory")]
		public List<Weapon> weaponsList;


		public delegate void WeaponChange(Weapon weapon); //delegate
		public event WeaponChange weaponChanged; //delegate instance


		private void Awake()
		{
			IncreaseInventorySize(info.MaxInventorySlots);
			if (this.equippedWeapon == null) { EquipWeapon(0); }
		}

		private void Start()
		{

		}

		public void AddWeaponToList(Weapon weapon)
		{
			switch (weaponsList.Count)
			{
				case 0:
					AssignWeaponToIndex(0, weapon);
					break;
				case 1:
					AssignWeaponToIndex(1, weapon);
					break;
				default:
					//overwrite equipped weapon
					AssignWeaponToIndex(weaponsList.IndexOf(equippedWeapon), weapon);
					break;
			}
		}

		private void EquipWeapon(int index)
		{
			Weapon weapon = weaponsList[index];
			if (weapon != null)
			{
				equippedWeapon = weapon;
				if (weaponChanged != null)
				{
					weaponChanged.Invoke(weapon);
				}
			}
		}

		//assign weapon to index of weapons list
		private void AssignWeaponToIndex(int index, Weapon weapon)
        {
			if (this.weaponsList[index] == null) return;
			this.weaponsList[index] = weapon;
        }

		private void IncreaseInventorySize(int size)
        {
            int slotsLeft = size - weaponsList.Count;
			if (slotsLeft < 0) return;
            for (int i = 0; i < slotsLeft; i++)
            {
				weaponsList.Add(null);
            }
		}
	}
}

