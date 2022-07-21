using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Player
{
	public class PlayerWeaponHandler : MonoBehaviour
	{
		[SerializeField,InfoBox("What to hit")]
		private LayerMask layerMask;

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
			return playerInventory.equippedWeapon.fireRate <= timer ? true : false;
		}

		private void Update()
		{
			IncreaseTimer();
			Shooting();
			equippedWeapon = playerInventory.equippedWeapon;
		}
		private void Shooting()
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
	}
}

