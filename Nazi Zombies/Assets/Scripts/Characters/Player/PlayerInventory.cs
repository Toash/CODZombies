using UnityEngine;
using System.Collections.Generic;


namespace Player
{
	[RequireComponent(typeof(PlayerWeaponHandler))]
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerInventory : MonoBehaviour
	{
		[HideInInspector]public Weapon equippedWeapon;

		[Header("Inventory")]
		public List<Weapon> weaponsList;

        [Header("Inventory Stats")]		
		public IntVariable maxWeapons;

		public delegate void WeaponChange(Weapon weapon); //delegate
		public event WeaponChange weaponChanged; //delegate instance

		private PlayerInput playerInput;

		private void Awake()
		{
			playerInput = this.GetComponent<PlayerInput>();
			IncreaseInventorySize(maxWeapons.Value);
			if (this.equippedWeapon == null) { EquipWeapon(0); }
		}

		private void Start()
		{

		}
		private void OnEnable()
		{
			playerInput.alpha1Clicked += EquipWeapon;
			playerInput.alpha2Clicked += EquipWeapon;
		}
		private void OnDisable()
		{
			playerInput.alpha1Clicked -= EquipWeapon;
			playerInput.alpha2Clicked -= EquipWeapon;
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

