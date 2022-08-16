using UnityEngine;
using Sirenix.OdinInspector;

namespace Player
{
	/// <summary>
    /// Read only, does not change at runtime
    /// </summary>
	[CreateAssetMenu]
	public class PlayerStats : ScriptableObject
	{
		[Title("Health")]
		public int MaxHealth;
		public int HealthRegenRate;

		[Title("Movement")]
		public float Speed;
		public float SprintMultiplier;
		public float JumpHeight;
		public float GravityMultiplier;

		[Title("Points")]
		public int StartingPoints;

		[Title("Inventory")]
		public int MaxInventorySlots;

		[Title("Interacting")]
		public float RaycastInteractRange;
		public float InteractSpeed;

	}
}