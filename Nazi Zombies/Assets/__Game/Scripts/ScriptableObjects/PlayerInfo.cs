using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
	//health
	public float StartingHealth;
	public float RegenRate;

	//movement
	public float Speed;
	public float JumpHeight;

	//camera
	public float Sensitivity;
	public int FOV;

	//inventory
	public int MaxInventorySlots;

	// misc
	public float GravityMultiplier;
	public float SlopeLimit;
}
