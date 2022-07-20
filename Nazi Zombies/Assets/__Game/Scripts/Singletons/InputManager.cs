using UnityEngine;
using System.Collections.Generic;


	public class InputManager : MonoBehaviour
	{
		private Dictionary<eInput, InputButton> VirtualButtons = new Dictionary<eInput, InputButton>();
		private Dictionary<eInput, InputAxes> VirtualAxes = new Dictionary<eInput, InputAxes>();

		public enum eInput
		{
			//movement
			INPUT_HORIZONTAL,
			INPUT_VERTICAL,
			INPUT_JUMP,
			INPUT_SPRINT,

			INPUT_INTERACT,

			//combat
			INPUT_SHOOT,
			INPUT_AIM,

			//inventory
			INPUT_EQUIP_PRIMARY,
			INPUT_EQUIP_SECONDARY,

			// mouse
			INPUT_MOUSEX,
			INPUT_MOUSEY
		}
		private void Awake()
		{

			VirtualButtons.Add(eInput.INPUT_JUMP, new InputButton(eInput.INPUT_JUMP));
			VirtualButtons.Add(eInput.INPUT_SPRINT, new InputButton(eInput.INPUT_SPRINT));

			VirtualButtons.Add(eInput.INPUT_INTERACT, new InputButton(eInput.INPUT_INTERACT));

			VirtualButtons.Add(eInput.INPUT_SHOOT, new InputButton(eInput.INPUT_SHOOT));
			VirtualButtons.Add(eInput.INPUT_AIM, new InputButton(eInput.INPUT_AIM));

			VirtualButtons.Add(eInput.INPUT_EQUIP_PRIMARY, new InputButton(eInput.INPUT_EQUIP_PRIMARY));
			VirtualButtons.Add(eInput.INPUT_EQUIP_SECONDARY, new InputButton(eInput.INPUT_EQUIP_SECONDARY));

			VirtualAxes.Add(eInput.INPUT_HORIZONTAL, new InputAxes(eInput.INPUT_HORIZONTAL));
			VirtualAxes.Add(eInput.INPUT_VERTICAL, new InputAxes(eInput.INPUT_VERTICAL));

			VirtualAxes.Add(eInput.INPUT_MOUSEX, new InputAxes(eInput.INPUT_MOUSEX));
			VirtualAxes.Add(eInput.INPUT_MOUSEY, new InputAxes(eInput.INPUT_MOUSEY));

		}


		private void Update()
		{
			keyboardInput();
			mouseInput();
		}



		private void keyboardInput()
		{
			// ----------- Movement --------------
			if (Input.GetButtonDown("Jump"))
			{
				VirtualButtons[eInput.INPUT_JUMP].Pressed();
			}
			if (Input.GetButtonUp("Jump"))
			{
				VirtualButtons[eInput.INPUT_JUMP].Released();
			}
			if (Input.GetButtonDown("Sprint"))
			{
				VirtualButtons[eInput.INPUT_SPRINT].Pressed();
			}
			if (Input.GetButtonUp("Sprint"))
			{
				VirtualButtons[eInput.INPUT_SPRINT].Released();
			}

			// ---------- Interact ----------

			if (Input.GetButtonDown("Interact"))
			{
				VirtualButtons[eInput.INPUT_INTERACT].Pressed();
			}
			if (Input.GetButtonUp("Interact"))
			{
				VirtualButtons[eInput.INPUT_INTERACT].Released();
			}

			//--------- Combat -----------

			if (Input.GetButtonDown("Shoot"))
			{
				VirtualButtons[eInput.INPUT_SHOOT].Pressed();
			}
			if (Input.GetButtonUp("Shoot"))
			{
				VirtualButtons[eInput.INPUT_SHOOT].Released();
			}
			if (Input.GetButtonDown("Aim"))
			{
				VirtualButtons[eInput.INPUT_AIM].Pressed();
			}
			if (Input.GetButtonUp("Aim"))
			{
				VirtualButtons[eInput.INPUT_AIM].Released();
			}
			if (Input.GetButtonDown("Primary"))
			{
				VirtualButtons[eInput.INPUT_EQUIP_PRIMARY].Pressed();
			}
			if (Input.GetButtonUp("Secondary"))
			{
				VirtualButtons[eInput.INPUT_EQUIP_SECONDARY].Released();
			}
			//Continuous input
			VirtualAxes[eInput.INPUT_HORIZONTAL].UpdateValue(Input.GetAxis("Horizontal"));
			VirtualAxes[eInput.INPUT_VERTICAL].UpdateValue(Input.GetAxis("Vertical"));
		}
		private void mouseInput()
		{
		//Continuous
			VirtualAxes[eInput.INPUT_MOUSEX].UpdateValue(Input.GetAxis("Mouse X"));
			VirtualAxes[eInput.INPUT_MOUSEY].UpdateValue(Input.GetAxis("Mouse Y"));
		}

		//other classes check if buttons are pressed
		public bool GetButton(eInput buttonID)
		{
			if (VirtualButtons.ContainsKey(buttonID))
			{
				return VirtualButtons[buttonID].GetButton;
			}
			else
			{
				Debug.LogError("No button id " + buttonID);
				return false;
			}
		}
		public bool GetButtonDown(eInput buttonID)
		{
			if (VirtualButtons.ContainsKey(buttonID))
			{
				return VirtualButtons[buttonID].GetButtonDown;
			}
			else
			{
				Debug.LogError("No button id " + buttonID);
				return false;
			}
		}
		public bool GetButtonUp(eInput buttonID)
		{
			if (VirtualButtons.ContainsKey(buttonID))
			{
				return VirtualButtons[buttonID].GetButtonUp;
			}
			else
			{
				Debug.LogError("No button id " + buttonID);
				return false;
			}
		}

		public float GetAxis(eInput axisID)
		{
			if (VirtualAxes.ContainsKey(axisID))
			{
				return VirtualAxes[axisID].GetValue;
			}
			else
			{
				Debug.LogError("Axis not found" + axisID);
				return 0;
			}
		}

		#region Helper Classes
		// Helper Class for button data frame over frame
		public class InputButton
		{
			public eInput id { get; private set; }
			private int lastPressedFrame = -5;
			private int releasedFrame = -5;
			private bool pressed = false;
			public InputButton(eInput id)
			{
				this.id = id;
			}
			// call when button is pressed
			public void Pressed()
			{
				if (pressed) return;
				pressed = true;
				lastPressedFrame = Time.frameCount;
			}
			//call when button is released
			public void Released()
			{
				pressed = false;
				releasedFrame = Time.frameCount;
			}
			public bool GetButton { get { return pressed; } }
			public bool GetButtonDown { get { return (lastPressedFrame - Time.frameCount) == -1; } }
			public bool GetButtonUp { get { return (releasedFrame - Time.frameCount) == -1; } }
		}

		//help class for axis data frame over frame
		public class InputAxes
		{
			public eInput id { get; private set; }
			private float value;
			public InputAxes(eInput id)
			{
				this.id = id;
			}
			// what is this for?
			// basically pass in unity's input system on here, 
			// there would be same functinoatlity if using unity input system directly,
			// but using it here gives more control?
			public void UpdateValue(float latestValue)
			{
				value = latestValue;
			}

			public float GetValue { get { return value; } }
			public float GetValueRaw { get { return value; } }
		}
		#endregion
	}



