using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour
	{
		public PlayerStats info;

		public enum State
		{
			GROUNDED,
			AIR,
		}

		private CharacterController charControl;
		private Vector3 playerGravityVelocity;

		void Awake()
		{
			charControl = this.GetComponent<CharacterController>();
			charControl.slopeLimit = info.SlopeLimit;
		}

		void Update()
		{
			charControl.Move(NormalizedMoveVector() * info.Speed*Time.deltaTime);
			HandleGravity();

			if (charControl.isGrounded)
			{
				if (ServiceLocator.Instance.InputManager.GetButtonDown(InputManager.eInput.INPUT_JUMP))
				{
					Jump();
				}
			}
		}
		public void Jump()
		{
			playerGravityVelocity.y = Mathf.Sqrt(info.JumpHeight * -2f * Physics.gravity.y);
		}

		private Vector3 NormalizedMoveVector()
		{
			Vector3 vertical = transform.forward * ServiceLocator.Instance.InputManager.GetAxis(InputManager.eInput.INPUT_VERTICAL_RAW);
			Vector3 horizontal = transform.right * ServiceLocator.Instance.InputManager.GetAxis(InputManager.eInput.INPUT_HORIZONTAL_RAW);
			return Vector3.Normalize(vertical + horizontal);
		}
		private void HandleGravity()
		{
			playerGravityVelocity += (Physics.gravity*info.GravityMultiplier) * Time.deltaTime;

			//set gravity to 0 when player is on the ground so i doesn't keep decreasing
			if (charControl.isGrounded && playerGravityVelocity.y < 0)
			{
				playerGravityVelocity.y = 0;
			}
			//Apply gravity to charactercontroller
			charControl.Move(playerGravityVelocity * Time.deltaTime);
		}
	}
}

