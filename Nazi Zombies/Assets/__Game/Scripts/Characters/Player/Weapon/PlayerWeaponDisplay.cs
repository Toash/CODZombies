using UnityEngine;

namespace Player
{
	public class PlayerWeaponDisplay : MonoBehaviour
	{
		[SerializeField] private PlayerInventory playerInventory;
		[SerializeField] private PlayerWeapon playerWeapon;
		[SerializeField] private Transform dynamicWeaponHoldPoint;

		private Vector3 cachedWeaponHoldPoint;

		private Vector3 recoil = new Vector3(0, 0, -10);

		private readonly float INTERPOLATION_SPEED = 5;
		private void OnEnable()
		{
			playerInventory.weaponChanged += DisplayWeapon;
			playerWeapon.GunFire += DisplayRecoil;
		}
		private void OnDisable()
		{
			playerInventory.weaponChanged -= DisplayWeapon;
			playerWeapon.GunFire -= DisplayRecoil;
		}
		private void Start()
		{
			cachedWeaponHoldPoint = dynamicWeaponHoldPoint.localPosition;
		}
        private void Update()
        {
			ReturnWeaponToNeutralPosition();
		}
        //---------DISPLAY-----------
        private void DisplayWeapon(Weapon weapon)
		{
			Debug.Log("Displaying weapon");
			Instantiate(weapon.model, dynamicWeaponHoldPoint.position, dynamicWeaponHoldPoint.rotation, dynamicWeaponHoldPoint);
		}
		private void DisplayRecoil()
		{
			dynamicWeaponHoldPoint.localPosition = Vector3.Lerp(dynamicWeaponHoldPoint.localPosition, dynamicWeaponHoldPoint.localPosition + recoil, Time.deltaTime * INTERPOLATION_SPEED);
		}
		private void ReturnWeaponToNeutralPosition()
		{
			dynamicWeaponHoldPoint.localPosition = Vector3.Lerp(dynamicWeaponHoldPoint.localPosition, cachedWeaponHoldPoint, Time.deltaTime * INTERPOLATION_SPEED);
		}
	}
}