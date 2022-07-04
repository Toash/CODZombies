using UnityEngine;

namespace Player
{
	//handles the player's currently equipped item
	[RequireComponent(typeof(PlayerInput))]
	[RequireComponent(typeof(PlayerCamera))]
	[RequireComponent(typeof(PlayerInventory))]
	[RequireComponent(typeof(AudioSource))]
	public class PlayerShooting : MonoBehaviour
	{
		[SerializeField]
		private GameObject weaponHoldPoint;

		private PlayerInput playerInput;
		private PlayerCamera playerCamera;
		private PlayerInventory playerInventory;
		private AudioSource gunShotAudioSource;

		private float timer;

		private void Awake()
		{
			playerInput = this.GetComponent<PlayerInput>();
			playerCamera = this.GetComponent<PlayerCamera>();
			playerInventory = this.GetComponent<PlayerInventory>();
			gunShotAudioSource = this.GetComponent<AudioSource>();
		}

		private void Update()
		{
			IncreaseTimer();
			if (playerInventory.equippedWeapon.automatic)
			{
				if (playerInput.LeftMouseHold && canFire())
				{
					//firing auto
					Shoot();
					ResetTimer();
				}
			}
			else
			{
				if((playerInput.LeftMouseClick!=playerInput.LeftMouseClick)&&canFire())
				//firing semi
				Shoot();
			}
		}

		private void IncreaseTimer()
		{
			timer += Time.deltaTime;
		}
		private void ResetTimer()
        {
			timer = 0;
        }
		private bool canFire()
		{
			return playerInventory.equippedWeapon.fireRate <= timer ? true : false;
		}
		private void Shoot()
		{
			Weapon weapon = playerInventory.equippedWeapon;
			Ballistics.CreateBullet(weapon.damage, playerCamera.getCameraRef().transform.position, playerCamera.getCameraRef().transform.forward, weapon.range);
			gunShotAudioSource.PlayOneShot(weapon.shootSound);
		}
	}
}
