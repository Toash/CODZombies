using UnityEngine;

namespace Player
{
	public class PlayerCamera : MonoBehaviour
	{
		public PlayerSettings settings;

		[SerializeField] private Camera cameraRef;
		[SerializeField] private Transform playerTransform;

		private float verticalRotation = 0f;
		private float horizontalRotation = 0f;

		public Camera getCameraRef()
		{
			return cameraRef;
		}

		void Awake()
		{
			//cameraRef.fieldOfView = settings.FOV;
			Cursor.lockState = CursorLockMode.Locked;
		}
		public void Update()
		{
			cameraMovement();
		}

		/// <summary>
		/// Move the camera, can be used to implement recoil.
		/// </summary>
		/// <param name="move"></param>
		public void ApplyCameraMovement(Vector2 move)
        {
			horizontalRotation += move.x;
			verticalRotation -= move.y;
        }
		private void cameraMovement()
		{
			// Horizontal player rotation
			horizontalRotation += (Input.GetAxis("Mouse X") * settings.Sensitivity);
			playerTransform.rotation = Quaternion.Euler(0, horizontalRotation, 0);

			// Vertical camera rotation
			verticalRotation -= (Input.GetAxis("Mouse Y") * settings.Sensitivity);
			verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
			cameraRef.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
		}
	}
}

