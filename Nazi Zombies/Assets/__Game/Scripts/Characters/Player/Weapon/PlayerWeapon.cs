using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Player
{
	public class PlayerWeapon : MonoBehaviour
	{

		[SerializeField] private PlayerCamera playerCamera;
		[SerializeField] private PlayerInventory playerInventory;
		[SerializeField] private AudioSource audioSource;

		public delegate void Action();
		public event Action GunFire;


		private float timer;

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
			if (Input.GetMouseButtonDown(0))
			{
				Shoot();
			}
		}


		private void Shoot()
		{
			//Ballistics.CreateBullet(playerCamera.getCameraRef().transform,equippedWeapon);
			GunFire?.Invoke();
			ServiceLocator.Instance.Ballistics.CastBullet(playerCamera.getCameraRef().transform, playerInventory.EquippedWeapon);
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

