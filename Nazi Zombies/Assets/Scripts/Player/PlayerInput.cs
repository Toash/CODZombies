using UnityEngine;
//gets player input, goes to NormalizedMoveVector
//gets mouse input too
namespace Player
{
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
        }
        private void mouseInput()
        {
            VerticalMouseInput = Input.GetAxis("Mouse Y");
            HorizontalMouseInput = Input.GetAxis("Mouse X");
        }
    }
}

