using UnityEngine;
using UnityEngine.Events;

namespace Player
{
	public class PlayerInput : MonoBehaviour
	{
		[Header("Custom")]
		public KeyCode jumpKey;
		public Vector3 NormalizedMoveVector { get; private set; }

		// mouse
		public float VerticalMouseInput { get; private set; }
		public float HorizontalMouseInput { get; private set; }

		// keyboard
		private float verticalKeyboardInput;
		private float horizontalKeyboardInput;

		// delegate
		public delegate void Click();
		public delegate void NumericalClick(int index);

		// C# events
		public event Click JumpClicked;
		public event Click LeftMouseDown;
		public event Click LeftMouseUp;
		public event NumericalClick Alpha1Clicked;
		public event NumericalClick Alpha2Clicked;

		private void Update()
		{
			keyboardInput();
			mouseInput();
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

		private void keyboardInput()
		{
			verticalKeyboardInput = Input.GetAxisRaw("Vertical");
			horizontalKeyboardInput = Input.GetAxisRaw("Horizontal");
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (Alpha1Clicked != null) { Alpha1Clicked(0); }

			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				if (Alpha2Clicked != null) { Alpha2Clicked(1); }

			}
			if (Input.GetKeyDown(jumpKey))
			{
				if (JumpClicked != null) { JumpClicked(); }
			}
		}
		private void mouseInput()
		{
			VerticalMouseInput = Input.GetAxis("Mouse Y");
			HorizontalMouseInput = Input.GetAxis("Mouse X");
			if (Input.GetMouseButtonDown(0))
			{
				if(LeftMouseDown!=null) LeftMouseDown();

			}
			if (Input.GetMouseButtonUp(0))
			{
				if (LeftMouseUp != null) LeftMouseUp();
			}
		}
	}
}

