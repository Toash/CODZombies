using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerMovement : MonoBehaviour
	{
		public PlayerStats stats;
		public PlayerSettings settings;

		[SerializeField]
		private Transform groundCheck;
		[SerializeField]
		private float groundCheckRadius = .4f;
		[SerializeField]
		private LayerMask groundMask;

		private CharacterController charControl;
		private Vector3 playerGravityVelocity;
		private float speedMultiplier = 1;

		void Awake()
		{
			charControl = GetComponent<CharacterController>();
		}

		void Update()
		{

			HandleSpeedMultiplier();
			HandleGravity();
			HandleMovement();
			
			if (isGrounded())
			{
				if (Input.GetButtonDown("Jump"))
				{
					Jump();
				}
			}
		}
        private void OnDrawGizmos()
        {
			Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        private bool isGrounded()
        {
			if (Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore))
			{
				// hit ground
				return true;
			}
			return false;
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
		private void HandleMovement()
        {
			charControl.Move(NormalizedMoveVector() * stats.Speed * speedMultiplier * Time.deltaTime);
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
		private void HandleSpeedMultiplier()
        {
            if (Input.GetKey(settings.Sprint))
            {
				speedMultiplier = stats.SprintMultiplier;
            }
            else
            {
				speedMultiplier = 1;
            }
        }
	}
}

