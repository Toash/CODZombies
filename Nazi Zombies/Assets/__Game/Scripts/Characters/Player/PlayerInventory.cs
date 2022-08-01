using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;


namespace Player
{
	public class PlayerInventory : MonoBehaviour
	{
		[ShowInInspector,ReadOnly,PropertyOrder(-1)]
		public Weapon EquippedWeapon { get; private set; }

		[SerializeField]
		private PlayerStats stats;

		[ValidateInput("ValidSize","Inventory must be valid size")]
		[Header("Inventory")]
		public List<Weapon> weaponsList;


		public delegate void WeaponChange(Weapon weapon); //delegate
		public event WeaponChange weaponChanged; //delegate instance


		private void Awake()
		{
			if (weaponsList.Count > stats.MaxInventorySlots) Debug.LogError("Too much weapons");
			IncreaseInventorySize(stats.MaxInventorySlots);

		}
		private void Start()
		{
			if (this.EquippedWeapon == null) { EquipWeapon(0); }
		}
		private void Update()
		{
			GetInput();
		}
		private void GetInput()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				EquipWeapon(0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				EquipWeapon(1);
			}
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
					AssignWeaponToIndex(weaponsList.IndexOf(EquippedWeapon), weapon);
					break;
			}
		}

		private void EquipWeapon(int index)
		{
			Weapon weapon = weaponsList[index];
			if (weapon != null)
			{
				EquippedWeapon = weapon;
				weaponChanged?.Invoke(weapon);

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
		private bool ValidSize()
        {
			return weaponsList.Count <= stats.MaxInventorySlots;
        }
	}
}

