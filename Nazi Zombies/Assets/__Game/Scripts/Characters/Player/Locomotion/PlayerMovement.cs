using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour
	{
		public PlayerStats stats;
		public PlayerSettings settings;

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
		}

		void Update()
		{
			charControl.Move(NormalizedMoveVector() * stats.Speed*Time.deltaTime);
			HandleGravity();

			if (charControl.isGrounded)
			{
				if (Input.GetButtonDown("Jump"))
				{
					Jump();
				}
			}
		}
		public void Jump()
		{
			playerGravityVelocity.y = Mathf.Sqrt(stats.JumpHeight * -2f * Physics.gravity.y);
		}

		private Vector3 NormalizedMoveVector()
		{
			Vector3 vertical = transform.forward * Input.GetAxisRaw("Vertical");
			Vector3 horizontal = transform.right * Input.GetAxisRaw("Horizontal");
			return Vector3.Normalize(vertical + horizontal);
		}
		private void HandleGravity()
		{
			playerGravityVelocity += (Physics.gravity*stats.GravityMultiplier) * Time.deltaTime;

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

