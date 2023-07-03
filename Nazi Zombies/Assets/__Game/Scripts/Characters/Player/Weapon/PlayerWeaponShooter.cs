using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Player
{
	/// <summary>
    /// Handles shooting the gun 
    /// </summary>
	public class PlayerWeaponShooter : MonoBehaviour
	{

		[SerializeField]
		private PlayerCamera playerCamera;
		[SerializeField]
		private PlayerInventory playerInventory;
		[SerializeField]
		private PlayerWeaponAmmo playerAmmo;

		public delegate void WeaponFire(WeaponStats weapon);
		/// <summary>
        /// Passes the weapon that was fired
        /// </summary>
		public event WeaponFire GunFireEvent;


		private float timer;

		private bool semiCanShoot;

		private bool canFire()
		{
			return cooldownUp() && playerAmmo.WeaponHasAmmoInMag(playerInventory.EquippedWeapon);
		}

		private bool cooldownUp()
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
			ServLoc.Inst.Ballistics.CastBullet(playerCamera.getCameraRef().transform, playerInventory.EquippedWeapon);
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

