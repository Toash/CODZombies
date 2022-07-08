using UnityEngine;
using UnityEngine.Events;
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

		[Header("Misc")]
		public StringVariable equippedWeaponSO;//scriptable object

		private PlayerInput playerInput;

		public delegate void Change();
		public event Change weaponChanged;
		public bool HasWeapon()
		{
			return equippedWeapon;
		}

		private void Awake()
		{
			playerInput = this.GetComponent<PlayerInput>();
			IncreaseInventorySizeTo(maxWeapons.Value);
			if (this.equippedWeapon==null)
			{
				EquipWeapon(0);//equip first weapon in list
			}
		}
		private void OnEnable()
		{
			playerInput.Alpha1Clicked += EquipWeapon;
			playerInput.Alpha2Clicked += EquipWeapon;
		}
		private void OnDisable()
		{
			playerInput.Alpha1Clicked -= EquipWeapon;
			playerInput.Alpha2Clicked -= EquipWeapon;
		}

		//equip to PlayerShooting
		public void EquipWeapon(int index)
		{
			if (weapons[index]!=null)
			{
				equippedWeapon = weapons[index];
				equippedWeaponSO.Value = weapons[index].name;// update the scriptableobject
				if(weaponChanged!=null) weaponChanged();
			}
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

