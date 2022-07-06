using UnityEngine;

namespace Zombie
{
	[RequireComponent(typeof(CapsuleCollider))]
	public class ZombieHealth : Health
	{
		protected override void die()
		{
			if (DeathEvent != null) { DeathEvent.Raise(); }
			Destroy(this.gameObject);
		}
	}

}
