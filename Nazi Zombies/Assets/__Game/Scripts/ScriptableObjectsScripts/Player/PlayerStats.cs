using UnityEngine;

namespace Player
{
	//Not exposable to the player
	[CreateAssetMenu]
	public class PlayerStats : ScriptableObject
	{
		//health
		public int MaxHealth;
		public int HealthRegenRate;

		//movement
		public float Speed;
		public float JumpHeight;
		public float GravityMultiplier;

		//points
		public int Points;

		//inventory
		public int MaxInventorySlots;

		//interact
		public float RaycastInteractRange;
		public float InteractSpeed;

	}
}