using UnityEngine;
using UnityEngine.Events;

namespace Player
{
	public class PlayerWeaponHandler : MonoBehaviour
	{
		[SerializeField]
		private LayerMask layerMask;

		private PlayerCamera playerCamera;
		private PlayerInventory playerInventory;
		private AudioSource audioSource;

		private Weapon equippedWeapon;

		public delegate void ShootDelegate();
		public delegate void AimDelegate(bool a);
		//delegate instances
		public event ShootDelegate gunShootEvent;
		public event AimDelegate isAimingEvent;


		private bool isShooting;
		private bool isAiming;
		private float timer;

		private bool canFire()
		{
			return playerInventory.equippedWeapon.fireRate <= timer ? true : false;
		}

		private void Awake()
		{
			playerCamera = this.GetComponent<PlayerCamera>();
			playerInventory = this.GetComponent<PlayerInventory>();
			audioSource = this.GetComponent<AudioSource>();
		}

		private void Update()
		{
			IncreaseTimer();
			Shooting();
			Aiming();
			equippedWeapon = playerInventory.equippedWeapon;
		}
		private void Shooting()
		{
			if (isShooting)
			{
				//auto
				if (equippedWeapon.automatic && canFire())
				{
					Shoot();
				}
				//semi
				else if (canFire())
				{
					Shoot();
					isShooting = false;
				}
			}
		}
		private void Aiming()
		{
			
		}


		private void Shoot()
		{
			Ballistics.CreateBullet(equippedWeapon.damage, playerCamera.getCameraRef().transform.position, playerCamera.getCameraRef().transform.forward, equippedWeapon.range, layerMask, QueryTriggerInteraction.Ignore);
			gunShootEvent?.Invoke(); 
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

		private void StartShooting()
		{
			isShooting = true;
		}
		private void StopShooting()
		{
			isShooting = false;
		}
		private void StartAiming()
		{
			isAiming = true;
			isAimingEvent?.Invoke(true);
		}
		private void StopAiming()
		{
			isAiming = false;
			isAimingEvent?.Invoke(false);
		}
	}
}

