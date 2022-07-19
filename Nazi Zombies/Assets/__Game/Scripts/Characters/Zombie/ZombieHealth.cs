using UnityEngine;
using UnityEngine.Events;

namespace AI.Zombie
{
	public class ZombieHealth : BaseHealth, IDamagable
	{
		public void KillZombie(float time)
		{
			Destroy(this.gameObject, time);
		}
	}
}

