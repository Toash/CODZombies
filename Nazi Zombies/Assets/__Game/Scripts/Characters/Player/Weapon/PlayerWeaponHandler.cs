using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Player
{
	public class PlayerWeaponHandler : MonoBehaviour
	{

		[SerializeField] private PlayerCamera playerCamera;

		[SerializeField] private PlayerInventory playerInventory;

		[SerializeField] private AudioSource audioSource;

		private Vector3 cachedWeaponHoldPoint; 

		private Vector3 recoil = new Vector3(0, 0, -10);

		private readonly float INTERPOLATION_SPEED = 5;

		private float timer;

		private bool canFire()
		{
			return playerInventory.EquippedWeapon.FireRate <= timer ? true : false;
		}

		private void OnEnable()
		{
			playerInventory.weaponChanged += DisplayWeapon;
		}
		private void OnDisable()
		{
			playerInventory.weaponChanged -= DisplayWeapon;
		}

		private void Update()
		{
			IncreaseTimer();
			ShootingLogic();
			ReturnWeaponToNeutralPosition();
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
			DisplayRecoil();
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
		//---------DISPLAY-----------
		private void DisplayWeapon(Weapon weapon)
		{
			Debug.Log("Displaying weapon");
			Instantiate(weapon.model, dynamicWeaponHoldPoint.position,dynamicWeaponHoldPoint.rotation,dynamicWeaponHoldPoint);
		}
		private void DisplayRecoil()
		{
			dynamicWeaponHoldPoint.localPosition = Vector3.Lerp(dynamicWeaponHoldPoint.localPosition, dynamicWeaponHoldPoint.localPosition+recoil, Time.deltaTime* INTERPOLATION_SPEED);
		}
		private void ReturnWeaponToNeutralPosition()
		{
			dynamicWeaponHoldPoint.localPosition = Vector3.Lerp(dynamicWeaponHoldPoint.localPosition, cachedWeaponHoldPoint, Time.deltaTime* INTERPOLATION_SPEED);
		}
	}
}

