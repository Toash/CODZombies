using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera cameraRef;
	public FloatVariable sensitivity;

	private Transform playerTransform;

	private float horizontalInput;
	private float verticalInput;

	private float verticalRotation = 0f;
	void Awake()
	{
		playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
	}

	public void Update()
	{
		cameraMovement();
		readMouseInput();
	}
	private void cameraMovement()
	{
		//rotate transform on the y axis. (left to right)
		playerTransform.Rotate(Vector3.up * horizontalInput * sensitivity.Value);

		verticalRotation -= (verticalInput * sensitivity.Value);

		//clamp
		verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
		//rotate cameras right axis (up and down)
		cameraRef.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
	}
	private void readMouseInput()
	{
		horizontalInput = Input.GetAxis("Mouse X");
		verticalInput = Input.GetAxis("Mouse Y");
	}
}
