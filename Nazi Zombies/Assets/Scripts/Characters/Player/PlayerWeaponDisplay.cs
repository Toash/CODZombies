using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(PlayerInput))]

	[RequireComponent(typeof(PlayerWeaponShooter))]
	public class PlayerWeaponDisplay : MonoBehaviour
	{
		[SerializeField]
		private GameObject point;//weapon hold


		private GameObject model;//weapon model
		private PlayerInput input;
		private PlayerInventory inv;

		private PlayerWeaponShooter shooter;


		private void Awake()
		{
			input = this.GetComponent<PlayerInput>();
			inv = this.GetComponent<PlayerInventory>();

			shooter = this.GetComponent<PlayerWeaponShooter>();
		}
		private void Update()
		{
			if (input.NormalizedMoveVector != Vector3.zero)
			{
				Debug.Log("moving");
			}
		}

		public void OnEnable()
		{
			//inventory
			inv.weaponChanged += DisplayWeapon;
		}
		public void OnDisable()
		{
			inv.weaponChanged -= DisplayWeapon;
		}

		private void DisplayWeapon(Weapon weapon)
		{
			if (weapon != null)
			{
				foreach (Transform child in point.transform) { Destroy(child.gameObject); }
				model = Instantiate(weapon.model, point.transform.position, Quaternion.LookRotation(-point.transform.forward), point.transform);
			}
		}
	}
}

