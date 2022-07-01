using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerShooting : MonoBehaviour
    {
		public Weapon equippedWeapon;

        private PlayerInput playerInput;
		private void Awake()
		{
			playerInput = this.GetComponent<PlayerInput>();
		}

		private void Update()
		{
			if (Input.GetMouseButton(0))
			{

			}
		}
	}
}

