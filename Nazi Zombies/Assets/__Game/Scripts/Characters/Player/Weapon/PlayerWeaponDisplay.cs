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
		[SerializeField,Tooltip("  hold point of weapon even when it moves around")] private Transform dynamicWeaponHoldPoint;
		[SerializeField] private float smoothing = 5;
		
		public float swayAmount = 0.01f;      // The amount of sway to be applied to the weapon
		public float maxSwayAmount = 0.01f;   // The maximum amount of sway


		private GameObject currentWeaponDisplay;

		private Vector3 initialPosition;

		private Vector3 recoil;



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
			initialPosition = dynamicWeaponHoldPoint.localPosition;
			
		}
        private void Update()
        {
	        WeaponSway();
	        LerpWeaponToInitialPos();
		}

        //---------DISPLAY-----------

		//Add the weapon sway onto weapon
		private void WeaponSway(){
			float mouseX = Input.GetAxis("Mouse X");
			float mouseY = Input.GetAxis("Mouse Y");

			// Calculate the new sway position based on mouse input
			float swayX = -mouseX * swayAmount;
			float swayY = -mouseY * swayAmount;
			swayX = Mathf.Clamp(swayX, -maxSwayAmount, maxSwayAmount);
			swayY = Mathf.Clamp(swayY, -maxSwayAmount, maxSwayAmount);

			Vector3 targetPosition = new Vector3(swayX, swayY, 0f) + initialPosition;
			Vector3 swayVec = new Vector3(swayX, swayY, 0);
			dynamicWeaponHoldPoint.localPosition += swayVec;
		}

        private void DisplayWeapon(WeaponStats weapon)
		{
			ClearDisplayWeapon();
			currentWeaponDisplay = Instantiate(weapon.Model, dynamicWeaponHoldPoint.position, dynamicWeaponHoldPoint.rotation, dynamicWeaponHoldPoint);
			recoil = new Vector3(0, 0, -playerInventory.EquippedWeapon.VisualRecoil);
			
		}
		private void ClearDisplayWeapon()
        {
			Destroy(currentWeaponDisplay);
        }
		private void DisplayRecoil(WeaponStats weapon)
		{
			
			dynamicWeaponHoldPoint.localPosition = Vector3.Lerp(dynamicWeaponHoldPoint.localPosition, dynamicWeaponHoldPoint.localPosition + recoil, Time.deltaTime * smoothing);
		}
		private void LerpWeaponToInitialPos()
		{
			dynamicWeaponHoldPoint.localPosition = Vector3.Lerp(dynamicWeaponHoldPoint.localPosition, initialPosition, Time.deltaTime * smoothing);
		}
	}
}