using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerCamera : MonoBehaviour
	{
		public FloatVariable sensitivity;
		public IntVariable playerFOV;

		[SerializeField] private Camera cameraRef;

		private PlayerInput playerInput;

		private float verticalRotation = 0f;

		public Camera getCameraRef()
		{
			return cameraRef;
		}

		void Awake()
		{
			playerInput = this.GetComponent<PlayerInput>();

			if (cameraRef == null) { Debug.LogError("No camera attached!!!"); }
			cameraRef.fieldOfView = playerFOV.Value;
			Cursor.lockState = CursorLockMode.Locked;
		}
		public void Update()
		{
			cameraMovement();
		}
		private void cameraMovement()
		{
			//rotate transform on the y axis. (left to right)
			this.transform.Rotate(Vector3.up * playerInput.HorizMouse * sensitivity.Value);

			verticalRotation -= (playerInput.VertMouse * sensitivity.Value);

			//clamp
			verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
			//rotate cameras right axis (up and down)
			cameraRef.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
		}
	}
}

