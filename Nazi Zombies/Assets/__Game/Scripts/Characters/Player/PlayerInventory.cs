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


		public delegate void WeaponChange(Weapon weapon); 
		public event WeaponChange weaponChanged; 


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
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				EquipWeapon(0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				EquipWeapon(1);
			}
		}

		/// <summary>
		/// Adds weapon to players inventory, if the inventory is full, overwrites equipped weapon.
		/// </summary>
		/// <param name="weapon"></param>
		public void AddWeaponToList(Weapon weapon)
		{
			bool playerHasMaxWeaponCapacity = WeaponCount() >= stats.MaxInventorySlots;

			if(playerHasMaxWeaponCapacity){
				Debug.Log("Inventory: Overwriting equipped weapon");
				AssignWeaponToIndex(weaponsList.IndexOf(EquippedWeapon), weapon);
			}
			switch (WeaponCount())
			{
				//No weapons
				case 0: 
					AssignWeaponToIndex(0, weapon);
					break;
				// Have one weapon, add to secondary slot
				case 1:
					AssignWeaponToIndex(1, weapon);
					break;
			}
			
			// have the player equip the new weapon after
			EquipWeapon(weaponsList.IndexOf(weapon));
		}

		/// <summary>
		/// Returns count of inventory
		/// </summary>
		/// <returns></returns>
		public int WeaponCount()
        {
			int count = 0;
			for(int i = 0; i < weaponsList.Count; i++)
            {
                if (weaponsList[i] != null)
                {
					count++;
                }
            }
			return count;
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
			this.weaponsList[index] = weapon;
        }

		/// <summary>
		/// Adds empty slots to match max inventory space.
		/// </summary>
		/// <param name="size"></param>
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
			return WeaponCount() <= stats.MaxInventorySlots;
        }
	}
}

