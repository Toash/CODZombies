using UnityEngine;

namespace AI.Zombie
{
	public interface IZombieTriggerEnter
	{
		public void TriggerEnter(ZombieStateManager zombie, Collider other);

	}
}