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
		[Tooltip("Accuracy for ground checker")]
		public FloatVariable groundedBias;

		private CharacterController charControl;
		private PlayerInput playerInput;

		private Vector3 playerVelocity;
		private bool isGrounded;

		void Awake()
		{
			//this == MonoBehaviour
			charControl = this.GetComponent<CharacterController>();
			playerInput = this.GetComponent<PlayerInput>();
		}


		void Update()
		{
			playerVelocity = ((playerInput.NormalizedMoveVector * moveSpeed.Value) + Physics.gravity) * Time.deltaTime;
			charControl.Move(playerVelocity);

			checkGround();
		}
		public void Jump()
		{
			if(isGrounded)
			{
				Debug.Log("Jumping");
				playerVelocity.y = Mathf.Sqrt(jumpHeight.Value * -2f*Physics.gravity.y);
				return;
			}
			Debug.Log("Cant jump, not grounded");
		}
		private void checkGround()
		{
			if ((charControl.velocity.y >= -groundedBias.Value) && (charControl.velocity.y <= groundedBias.Value))
			{
				isGrounded = true;
			}
			else
			{
				isGrounded = false;
			}
		}
	}
}

