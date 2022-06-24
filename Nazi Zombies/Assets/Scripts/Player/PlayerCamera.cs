using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
	[SerializeField] private float sensitivity;

	private Transform playerTransform;

	private float horizontalInput;
	private float verticalInput;

	private float verticalRotation = 0f;

	public Camera getCamera()
	{
		return this.camera;
	}

	void Awake()
	{
		playerTransform = this.gameObject.GetComponent<Transform>();

		if (this.camera == null)
		{
			Debug.LogError("Camera isn't assigned to " + this.name);
		}
	}

	public void Update()
	{
		readInput();
		cameraMovement();
		if (Input.GetMouseButtonDown(0))
		{
			lockCursor();
		}
	}
	private void cameraMovement()
	{
		//rotate transform on the y axis. (left to right)
		playerTransform.Rotate(Vector3.up * horizontalInput * sensitivity);
		//rotate cameras right axis (up and down)
		//camera.transform.Rotate(Vector3.right * -vertical * sensitivity);

		verticalRotation -= (verticalInput * sensitivity);
		verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
		camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
	}


	private void readInput()
	{
		horizontalInput = Input.GetAxis("Mouse X");
		verticalInput = Input.GetAxis("Mouse Y");
	}
	private void lockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
}
