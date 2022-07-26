using UnityEngine;

namespace AI.Zombie
{
	public interface IZombieTriggerStay
	{
		public void TriggerStay(ZombieStateManager zombie, Collider other);
	}
}