using UnityEngine;
using UnityEngine.Events;

namespace Player
{
	//handles the player's currently equipped item
	[RequireComponent(typeof(PlayerCamera))] //to create bullet in camera
	[RequireComponent(typeof(PlayerInventory))]// to get equipped weapon
	[RequireComponent(typeof(PlayerInput))] // to get left mouse button
	[RequireComponent(typeof(AudioSource))] // for gun sound
	[System.Serializable]
	public class WeaponFireEvent : UnityEvent<Weapon>
	{
	}
	public class PlayerWeaponShooter : MonoBehaviour
	{
		public WeaponFireEvent weaponFireEvent;

		[SerializeField]
		private LayerMask layerMask;

		private PlayerInput playerInput;
		private PlayerCamera playerCamera;
		private PlayerInventory playerInventory;
		private AudioSource audioSource;

		private Weapon equippedWeapon;

		public delegate void ShootDelegate();
		public event ShootDelegate gunShoot;


		private bool isShooting;
		private float timer;

		private bool canFire()
		{
			return playerInventory.equippedWeapon.fireRate <= timer ? true : false;
		}

		private void Awake()
		{
			playerInput = this.GetComponent<PlayerInput>();
			playerCamera = this.GetComponent<PlayerCamera>();
			playerInventory = this.GetComponent<PlayerInventory>();
			audioSource = this.GetComponent<AudioSource>();
		}

		private void OnEnable()
		{
			//input
			playerInput.shootDown += StartShooting;
			playerInput.shootUp += StopShooting;
		}
		private void OnDisable()
		{
			playerInput.shootDown -= StartShooting;
			playerInput.shootUp -= StopShooting;
		}

		private void Update()
		{
			IncreaseTimer();
			Shooting();
			equippedWeapon = playerInventory.equippedWeapon;
		}
		public void Shooting()
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

		private void Shoot()
		{
			Ballistics.CreateBullet(equippedWeapon.damage, playerCamera.getCameraRef().transform.position, playerCamera.getCameraRef().transform.forward, equippedWeapon.range, layerMask, QueryTriggerInteraction.Ignore);
			weaponFireEvent?.Invoke(equippedWeapon);
			gunShoot?.Invoke(); 
			ResetTimer();
		}
		private void StartShooting()
		{
			isShooting = true;
		}
		private void StopShooting()
		{
			isShooting = false;
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

