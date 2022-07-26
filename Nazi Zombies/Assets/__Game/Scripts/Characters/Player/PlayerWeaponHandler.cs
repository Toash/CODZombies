using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Player
{
	public class PlayerWeaponHandler : MonoBehaviour
	{
		[SerializeField]
		private PlayerCamera playerCamera;
		[SerializeField]
		private PlayerInventory playerInventory;
		[SerializeField,InfoBox("Where the Gun Sound will play")]
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
			return playerInventory.equippedWeapon.FireRate <= timer ? true : false;
		}

		private void Update()
		{
			IncreaseTimer();
			Shooting();
			equippedWeapon = playerInventory.equippedWeapon;
		}
		private void Shooting()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Shoot();
			}
		}

		private void Shoot()
		{
			//Ballistics.CreateBullet(playerCamera.getCameraRef().transform,equippedWeapon);
			ServiceLocator.Instance.Ballistics.CastBullet(playerCamera.getCameraRef().transform, equippedWeapon);
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
	}
}

