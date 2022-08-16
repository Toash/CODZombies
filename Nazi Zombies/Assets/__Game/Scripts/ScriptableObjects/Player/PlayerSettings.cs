using System.Collections;
using UnityEngine;

namespace Player
{
	//these fields should be exposed to the player in a menu
	[CreateAssetMenu]
	public class PlayerSettings : ScriptableObject
	{
		//keybinds
		[Header("Keybinds")]
		public KeyCode Sprint;
		public KeyCode Interact;
		public KeyCode Crouch;
		public KeyCode Jump;
		//camera
		public float Sensitivity;
		public int FOV;
	}
}