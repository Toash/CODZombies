using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
	//health
	public float Health;
	public float HealthRegenRate;

	//movement
	public float Speed;
	public float JumpHeight;
	public float GravityMultiplier;

	//points
	public int Points;



	//inventory
	public int MaxInventorySlots;

	//interact
	public float InteractRange;

}
