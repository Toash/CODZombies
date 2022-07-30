using UnityEngine;

namespace Player
{
	/// <summary>
    /// Handles all the visuls of the weapon. Recoil, Model etc
    /// </summary>
	public class PlayerWeaponDisplay : MonoBehaviour
	{
		[SerializeField] private PlayerInventory playerInventory;
		[SerializeField] private PlayerWeaponShooter playerWeapon;
		[SerializeField] private Transform dynamicWeaponHoldPoint;

		private GameObject currentWeaponDisplay;

		private Vector3 cachedWeaponHoldPoint;

		private Vector3 recoil;

		private readonly float INTERPOLATION_SPEED = 5;

		private void OnEnable()
		{
			playerInventory.weaponChanged += DisplayWeapon;
			playerWeapon.GunFireEvent += DisplayRecoil;
		}
		private void OnDisable()
		{
			playerInventory.weaponChanged -= DisplayWeapon;
			playerWeapon.GunFireEvent -= DisplayRecoil;
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
			ClearDisplayWeapon();
			currentWeaponDisplay = Instantiate(weapon.model, dynamicWeaponHoldPoint.position, dynamicWeaponHoldPoint.rotation, dynamicWeaponHoldPoint);
			recoil = new Vector3(0, 0, -playerInventory.EquippedWeapon.VisualRecoil);
			
		}
		private void ClearDisplayWeapon()
        {
			Destroy(currentWeaponDisplay);
        }
		private void DisplayRecoil(Weapon weapon)
		{
			dynamicWeaponHoldPoint.localPosition = Vector3.Lerp(dynamicWeaponHoldPoint.localPosition, dynamicWeaponHoldPoint.localPosition + recoil, Time.deltaTime * INTERPOLATION_SPEED);
		}
		private void ReturnWeaponToNeutralPosition()
		{
			dynamicWeaponHoldPoint.localPosition = Vector3.Lerp(dynamicWeaponHoldPoint.localPosition, cachedWeaponHoldPoint, Time.deltaTime * INTERPOLATION_SPEED);
		}
	}
}