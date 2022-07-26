using UnityEngine;

namespace AI.Zombie
{
	public interface IZombieTriggerExit
	{
		public void TriggerExit(ZombieStateManager zombie, Collider other);
	}
}