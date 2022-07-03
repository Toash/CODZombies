using UnityEngine;
using System.Collections.Generic;

namespace Player
{
	[RequireComponent(typeof(PlayerShooting))]
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerInventory : MonoBehaviour
	{
		[Header("The Currently Equipped Weapon")]
		public Weapon equippedWeapon;

		[Header("Inventory")]
		public List<Weapon> weapons;

        [Header("Inventory Stats")]		
		public IntVariable maxWeapons;

		public bool HasWeapon()
		{
			return equippedWeapon;
		}

		private void Awake()
		{
			IncreaseInventorySizeTo(maxWeapons.Value);
			if (this.equippedWeapon==null)
			{
				EquipWeapon(0);//equip first weapon in list
			}
		}
		//equip to PlayerShooting
		public void EquipWeapon(int index)
		{
			equippedWeapon = weapons[index];
		}

		//Add weapon to weapons list
		public void AddWeapon(Weapon weapon)
		{
            //There are weapon slots left
            switch (weapons.Count)
            {
				case 0:
					AssignWeapon(0, weapon);
					break;
				case 1:
					AssignWeapon(1, weapon);
					break;
				default:
					//overwrite equipped weapon
					int index = weapons.IndexOf(equippedWeapon);
					AssignWeapon(index, weapon);
					break;
			}

		}

		//assign weapon to index of weapons list
		private void AssignWeapon(int index, Weapon weapon)
        {
			this.weapons[index] = weapon;
        }

		private void IncreaseInventorySizeTo(int size)
        {
            int slotsLeft = size - weapons.Count;
			if (slotsLeft < 0) return;
            for (int i = 0; i < slotsLeft; i++)
            {
				weapons.Add(null);
            }
		}
	}
}

