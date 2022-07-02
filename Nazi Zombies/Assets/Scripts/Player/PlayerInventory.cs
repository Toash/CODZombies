using UnityEngine;
using System.Collections.Generic;

namespace Player
{
	[RequireComponent(typeof(PlayerShooting))]
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerInventory : MonoBehaviour
	{
		public List<Weapon> weapons = new List<Weapon>();

		[SerializeField]
		private int maxWeapons = 2;

		private int currentIndex;
		private PlayerShooting playerShooting;

		private void Awake()
		{
			playerShooting = this.GetComponent<PlayerShooting>();
		}
		private void Update()
		{
			GetInput();
		}

		public void EquipWeapon(int index)
		{
		}

		public void AddWeapon(Weapon weapon)
		{
			int size = weapons.Count;
		}
		private void GetInput()
		{
			
		}
	}
}

