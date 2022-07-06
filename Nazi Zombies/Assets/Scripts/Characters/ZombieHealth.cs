
using UnityEngine;

namespace Zombie
{
    public class ZombieHealth : BaseHealth
    {
		protected override void die()
		{
			Destroy(this.gameObject, 3);
		}
	}
}

