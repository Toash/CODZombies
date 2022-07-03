using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {

        public Vector3 NormalizedMoveVector { get; private set; }

        public float VerticalMouseInput { get; private set; }
        public float HorizontalMouseInput { get; private set; }

        public bool LeftMouseHold { get; private set; }//continuous inputs are hidden in inspector
        public bool LeftMouseClick { get; private set; }//continuous inputs are hidden in inspector
        //keyboard
        private float verticalKeyboardInput;
        private float horizontalKeyboardInput;

        [Header("Keyboard Bindings")]
        [SerializeField]
        private KeyCode jumpKey;

        [Header("Keyboard Events")]
        [SerializeField]
        private UnityEvent Alpha1Event; //when player presses 1, etc.
        [SerializeField]
        private UnityEvent Alpha2Event;
        [SerializeField]
        private UnityEvent JumpEvent;

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
                Alpha1Event.Invoke();
			}
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Alpha1Event.Invoke();
            }
			if (Input.GetKeyDown(jumpKey))
			{
                //jump
                JumpEvent.Invoke();
			}
        }
        private void mouseInput()
        {
            VerticalMouseInput = Input.GetAxis("Mouse Y");
            HorizontalMouseInput = Input.GetAxis("Mouse X");
            LeftMouseHold = Input.GetMouseButton(0) ? true : false;
			if (Input.GetMouseButtonDown(0)) { LeftMouseClick = !LeftMouseClick; }
        }
    }
}

