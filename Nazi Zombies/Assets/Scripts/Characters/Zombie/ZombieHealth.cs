
using UnityEngine;

namespace Zombie
{
    public class ZombieHealth : BaseHealth
    {
		protected override void die()
		{
			Debug.Log("Zombie dead yo!!!");
			Destroy(this.gameObject);
		}
	}
}

