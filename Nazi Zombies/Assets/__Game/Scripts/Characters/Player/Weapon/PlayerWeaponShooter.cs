using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Player
{
	/// <summary>
    /// Handles shooting the gun (input, raycast, shoot event, etc) 
    /// </summary>
	public class PlayerWeaponShooter : MonoBehaviour
	{

		[SerializeField]
		private PlayerCamera playerCamera;
		[SerializeField]
		private PlayerInventory playerInventory;

		public delegate void WeaponFire(Weapon weapon);
		/// <summary>
        /// Passes the weapon that was fired
        /// </summary>
		public event WeaponFire GunFireEvent;


		private float timer;

		private bool semiCanShoot;

		private bool canFire()
		{
			return playerInventory.EquippedWeapon.FireRate <= timer ? true : false;
		}

		

		private void Update()
		{
			IncreaseTimer();
			ShootingLogic();

		}
		private void ShootingLogic()
		{
			if (Input.GetMouseButton(0)&&canFire())
			{
				if (playerInventory.EquippedWeapon.Automatic == true)
                {
					Shoot();
					return; 
				}
				//semi
				if (semiCanShoot)
                {
					Shoot();
					semiCanShoot = false;
				}
			}
            if (Input.GetMouseButtonUp(0))
            {
				semiCanShoot = true;
            }
		}


		private void Shoot()
		{
			//Ballistics.CreateBullet(playerCamera.getCameraRef().transform,equippedWeapon);
			GunFireEvent?.Invoke(playerInventory.EquippedWeapon);
			ServLoc.I.Ballistics.CastBullet(playerCamera.getCameraRef().transform, playerInventory.EquippedWeapon);
			ResetTimer();
		}

		private void IncreaseTimer()
		{
			timer += Time.deltaTime;
		}
		private void ResetTimer()
		{
			timer = 0;
		}

	}
}

