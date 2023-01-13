using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	public class ZombieHealth : MonoBehaviour, IDamagable
	{
		public int StartingHealth = 100;

		[SerializeField] private UnityEvent DamagedEvent;
		[SerializeField] private UnityEvent NoHealthEvent;

		[PropertyOrder(-1),ShowInInspector,ReadOnly]
		private int currentHealth;

		private bool noHealth { get { return currentHealth <= 0; } }

        private void Awake()
        {
			currentHealth = StartingHealth;
        }
		
		public virtual void Damage(int amount)
		{
			currentHealth -= amount;
			DamagedEvent?.Invoke();
			if (noHealth) NoHealthEvent?.Invoke();
		}
		public void KillZombie(float time)
		{
			Destroy(gameObject, time);
		}
	}

}

