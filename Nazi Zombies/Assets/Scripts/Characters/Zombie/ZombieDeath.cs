using UnityEngine;

namespace AI.Zombie
{
    public class ZombieDeath : MonoBehaviour
    {
        public void KillZombie(float time)
		{
            Destroy(this.gameObject, time);
		}
    }
}

