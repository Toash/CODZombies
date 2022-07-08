using UnityEngine;
using UnityEngine.Events;

namespace Player
{
	//handles the player's currently equipped item
	[RequireComponent(typeof(PlayerCamera))] //to create bullet in camera
	[RequireComponent(typeof(PlayerInventory))]// to get equipped weapon
	[RequireComponent(typeof(PlayerInput))] // to get left mouse button
	[RequireComponent(typeof(AudioSource))] // for gun sound
	public class PlayerShooting : MonoBehaviour
	{
		public UnityEvent playerShoot;

		[SerializeField]
		private LayerMask layerMask;
		[SerializeField]
		private GameObject weaponHoldPoint;

		private PlayerInput playerInput;
		private PlayerCamera playerCamera;
		private PlayerInventory playerInventory;
		private AudioSource audioSource;

		private Weapon equippedWeapon;

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
			playerInput.LeftMouseDown += StartShooting;
			playerInput.LeftMouseUp += StopShooting;

			//inventory
			playerInventory.weaponChanged += DisplayWeapon;
		}
		private void OnDisable()
		{
			playerInput.LeftMouseDown -= StartShooting;
			playerInput.LeftMouseUp -= StopShooting;

			playerInventory.weaponChanged -= DisplayWeapon;
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
		private void StartShooting()
		{
			isShooting = true;
		}
		private void StopShooting()
		{
			isShooting = false;
		}

		private void Shoot()
		{
			Ballistics.CreateBullet(equippedWeapon.damage, playerCamera.getCameraRef().transform.position, playerCamera.getCameraRef().transform.forward, equippedWeapon.range, layerMask, QueryTriggerInteraction.Ignore);
			audioSource.PlayOneShot(equippedWeapon.shootSound);
			playerShoot.Invoke();
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

		private void DisplayWeapon()
		{
			equippedWeapon.DeleteDisplayWeapon();
			equippedWeapon.DisplayWeapon(weaponHoldPoint.transform);
		}

	}
}

