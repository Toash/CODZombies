using UnityEngine;
//gets player input, goes to NormalizedMoveVector
//gets mouse input too
public class PlayerInput : MonoBehaviour
{
    public Vector3 NormalizedMoveVector { get; private set; }

    public float VerticalMouseInput { get; private set; }
    public float HorizontalMouseInput { get; private set; }
    //keyboard
    private float verticalKeyboardInput;
    private float horizontalKeyboardInput;

    private void Update()
	{
        verticalKeyboardInput = Input.GetAxisRaw("Vertical");
        horizontalKeyboardInput = Input.GetAxisRaw("Horizontal");

        VerticalMouseInput = Input.GetAxis("Mouse Y");
        HorizontalMouseInput = Input.GetAxis("Mouse X");

        calculateMoveVector();
	}

	private void calculateMoveVector()
    {
        Vector3 vertical = this.transform.forward * verticalKeyboardInput;
        Vector3 horizontal = this.transform.right * horizontalKeyboardInput;

        // normalize so player cant run double as 
        // fast when moving diagonally
        NormalizedMoveVector = Vector3.Normalize(vertical + horizontal);
    }
}
