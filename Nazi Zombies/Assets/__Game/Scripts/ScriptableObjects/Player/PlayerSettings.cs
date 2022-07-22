using System.Collections;
using UnityEngine;

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
