using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Camera cameraRef;
	public FloatVariable sensitivity;

	private PlayerInput playerInput;

	private float verticalRotation = 0f;

	void Awake()
	{
		playerInput = this.GetComponent<PlayerInput>();
	}
	public void Update()
	{
		cameraMovement();
	}
	private void cameraMovement()
	{
		//rotate transform on the y axis. (left to right)
		this.transform.Rotate(Vector3.up * playerInput.HorizontalMouseInput * sensitivity.Value);

		verticalRotation -= (playerInput.VerticalMouseInput * sensitivity.Value);

		//clamp
		verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
		//rotate cameras right axis (up and down)
		cameraRef.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
	}
}
