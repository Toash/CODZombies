using UnityEngine;
using System.Collections.Generic;

namespace Player
{
	[RequireComponent(typeof(PlayerShooting))]
	public class PlayerInventory : MonoBehaviour
	{
		public List<Weapon> weapons = new List<Weapon>();

		[SerializeField]
		private int maxWeapons = 2;

		private PlayerShooting playerShooting;

		public void EquipWeapon(Weapon weapon)
		{
		}

		public void AddWeapon(Weapon weapon)
		{
			int size = weapons.Count;
		}
	}
}

