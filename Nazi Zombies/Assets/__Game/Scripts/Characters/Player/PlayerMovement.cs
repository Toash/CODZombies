using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour
	{
		public PlayerInfo info;

		private CharacterController charControl;

		private Vector3 playerGravityVelocity;

		void Awake()
		{
			charControl = this.GetComponent<CharacterController>();
			charControl.slopeLimit = info.SlopeLimit;
		}

		void Update()
		{
			//multiplying my deltaTime means it is framerate independent
			charControl.Move(NormalizedMoveVector() * info.Speed*Time.deltaTime);
			SetPlayerGravity();
			ResetPlayerGravity();
			charControl.Move(playerGravityVelocity * Time.deltaTime);
		}
		public void Jump()
		{
			if (charControl.isGrounded)
			{
				//jump
				playerGravityVelocity.y = Mathf.Sqrt(info.JumpHeight * -2f * Physics.gravity.y);
				return;
			}
			//not grounded
		}

		private Vector3 NormalizedMoveVector()
		{
			Vector3 vertical = this.transform.forward * ServiceLocator.Instance.InputManager.GetAxis(InputManager.eInput.INPUT_VERTICAL);
			Vector3 horizontal = this.transform.right * ServiceLocator.Instance.InputManager.GetAxis(InputManager.eInput.INPUT_HORIZONTAL);

			// normalize so player cant run double as 
			// fast when moving diagonally
			return Vector3.Normalize(vertical + horizontal);
		}
		private void SetPlayerGravity()
		{
			playerGravityVelocity += (Physics.gravity*info.GravityMultiplier) * Time.deltaTime;
		}

		//set gravity to 0 when player is on the ground so i doesn't keep decreasing
		private void ResetPlayerGravity()
		{
			if (charControl.isGrounded&&playerGravityVelocity.y<0)
			{
				playerGravityVelocity.y = 0;
			}
		}
	}
}

