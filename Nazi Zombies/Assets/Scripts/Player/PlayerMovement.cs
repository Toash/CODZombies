using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerMovement : MonoBehaviour
	{
		[Header("Movement Stats")]
		public FloatVariable moveSpeed;
		public FloatVariable jumpHeight;
		[Header("Misc")]
		public FloatVariable playerGravityMultiplier;
		public FloatVariable playerSlopeLimit;

		private CharacterController charControl;
		private PlayerInput playerInput;

		private Vector3 playerGravityVelocity;

		void Awake()
		{
			charControl = this.GetComponent<CharacterController>();
			playerInput = this.GetComponent<PlayerInput>();
			charControl.slopeLimit = playerSlopeLimit.Value;
		}

		void Update()
		{
			//multiplying my deltaTime means it is framerate independent
			charControl.Move(playerInput.NormalizedMoveVector * moveSpeed.Value*Time.deltaTime);
			SetPlayerGravity();
			ResetPlayerGravity();
			charControl.Move(playerGravityVelocity * Time.deltaTime);
		}
		public void Jump()
		{
			if (charControl.isGrounded)
			{
				//jump
				playerGravityVelocity.y = Mathf.Sqrt(jumpHeight.Value * -2f * Physics.gravity.y);
				return;
			}
			//not grounded
		}
		private void SetPlayerGravity()
		{
			playerGravityVelocity += (Physics.gravity*playerGravityMultiplier.Value) * Time.deltaTime;
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

