using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerWeaponDisplay : MonoBehaviour
	{
		[SerializeField]
		private GameObject point;//weapon hold

		private GameObject model;//weapon model
		private PlayerInput input;
		private PlayerInventory inv;


		private void Awake()
		{
			input = this.GetComponent<PlayerInput>();
			inv = this.GetComponent<PlayerInventory>();
		}
		private void Update()
		{
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
		public void DisplayWeapon(Weapon weapon)
		{
			if (weapon != null)
			{
				Debug.Log("Changed weapon!");
				//delete child objects of weaponHoldPoint first
				foreach (Transform child in point.transform) { Destroy(child.gameObject); }
				model = Instantiate(weapon.model, point.transform.position, Quaternion.LookRotation(-point.transform.forward), point.transform);
			}
			Debug.Log("Cant Changed weapon!");
		}
	}
}

