using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerMover : MonoBehaviour
	{
		public FloatVariable moveSpeed;

		private CharacterController characterController;
		private PlayerInput playerInput;

		void Awake()
		{
			//this == MonoBehaviour
			characterController = this.GetComponent<CharacterController>();
			playerInput = this.GetComponent<PlayerInput>();
		}


		void Update()
		{
			characterController.Move(((playerInput.NormalizedMoveVector * moveSpeed.Value) + Physics.gravity) * Time.deltaTime);
		}
	}
}

