using UnityEngine;
using UnityEngine.Events;

namespace Player
{
	//handles the player's currently equipped item
	[RequireComponent(typeof(PlayerCamera))]
	[RequireComponent(typeof(PlayerInventory))]
	[RequireComponent(typeof(PlayerInput))]
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
		}

		private void OnEnable()
		{
			playerInput.LeftMouseDown += StartShooting;
			playerInput.LeftMouseUp += StopShooting;
		}
		private void OnDisable()
		{
			playerInput.LeftMouseDown -= StartShooting;
			playerInput.LeftMouseUp -= StopShooting;
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


	}
}

