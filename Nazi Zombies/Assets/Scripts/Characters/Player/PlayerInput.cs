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
		//keyboard 
		public event Click jumpClicked;
		public event NumericalClick alpha1Clicked;
		public event NumericalClick alpha2Clicked;
		public event Click interactClicked;

		//mouse
		public event Click shootDown;
		public event Click shootUp;
		public event Click aimDown;
		public event Click aimUp;



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
				alpha1Clicked?.Invoke(0);

			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				alpha2Clicked?.Invoke(1);

			}
			if (Input.GetKeyDown(jumpKey))
			{
				jumpClicked?.Invoke();
			}
			if (Input.GetKeyDown(interactKey))
			{
				interactClicked?.Invoke();
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
			if (Input.GetKeyDown(aimKey))
			{
				aimDown?.Invoke();
			}
			if (Input.GetKeyUp(aimKey))
			{
				aimUp?.Invoke();
			}
		}
	}
}

