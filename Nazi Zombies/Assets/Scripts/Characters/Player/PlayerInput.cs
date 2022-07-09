using UnityEngine;
using UnityEngine.Events;

namespace Player
{
	public class PlayerInput : MonoBehaviour
	{
		[Header("Bindings")]
		public KeyCode jumpKey;
		public KeyCode shootKey;
		public KeyCode aimKey;
		public KeyCode interactKey;
		public KeyCode sprintKey;

		public Vector3 NormalizedMoveVector { get; private set; }

		// mouse
		public float VertMouse { get; private set; }
		public float HorizMouse { get; private set; }

		// keyboard
		public float VertKeyboard { get; private set; }
		public float HorizKeyboard { get; private set; }

		// delegate
		public delegate void Click();
		public delegate void NumericalClick(int index);

		// C# events
		public event Click jumpClicked;
		public event Click shootDown;
		public event Click shootUp;
		public event NumericalClick alpha1Clicked;
		public event NumericalClick alpha2Clicked;

		private void Update()
		{
			keyboardInput();
			mouseInput();
			calculateMoveVector();
		}

		private void calculateMoveVector()
		{
			Vector3 vertical = this.transform.forward * VertKeyboard;
			Vector3 horizontal = this.transform.right * HorizKeyboard;

			// normalize so player cant run double as 
			// fast when moving diagonally
			NormalizedMoveVector = Vector3.Normalize(vertical + horizontal);
		}

		private void keyboardInput()
		{
			VertKeyboard = Input.GetAxisRaw("Vertical");
			HorizKeyboard = Input.GetAxisRaw("Horizontal");
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (alpha1Clicked != null) { alpha1Clicked(0); }

			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				if (alpha2Clicked != null) { alpha2Clicked(1); }

			}
			if (Input.GetKeyDown(jumpKey))
			{
				if (jumpClicked != null) { jumpClicked(); }
			}
		}
		private void mouseInput()
		{
			VertMouse = Input.GetAxis("Mouse Y");
			HorizMouse = Input.GetAxis("Mouse X");
			if (Input.GetKeyDown(shootKey))
			{
				shootDown?.Invoke();

			}
			if (Input.GetKeyUp(shootKey))
			{
				shootUp?.Invoke();
			}
		}
	}
}

