using UnityEngine;

namespace Player
{
	public class PlayerCamera : MonoBehaviour
	{
		public PlayerInfo info;

		[SerializeField] private Camera cameraRef;
		[SerializeField] private Transform playerTransform;


		private float verticalRotation = 0f;

		public Camera getCameraRef()
		{
			return cameraRef;
		}

		void Awake()
		{
			if (cameraRef == null) { Debug.LogError("No camera attached!!!"); }
			cameraRef.fieldOfView = info.FOV;
			Cursor.lockState = CursorLockMode.Locked;
		}
		public void Update()
		{
			cameraMovement();
		}
		private void cameraMovement()
		{
			//rotate transform on the y axis. (left to right)
			playerTransform.Rotate(Vector3.up * ServiceLocator.Instance.InputManager.GetAxis(InputManager.eInput.INPUT_MOUSEX) * info.Sensitivity);

			verticalRotation -= (ServiceLocator.Instance.InputManager.GetAxis(InputManager.eInput.INPUT_MOUSEY) * info.Sensitivity);

			//clamp
			verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
			//rotate cameras right axis (up and down)
			cameraRef.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
		}
	}
}

