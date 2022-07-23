using UnityEngine;
using UnityEngine.Events;

namespace AI.Zombie
{
	public class ZombieHealth : MonoBehaviour, IDamagable
	{
		[SerializeField]
		private ZombieStats stats;

		[Header("Unity Events")]
		[SerializeField]
		private UnityEvent DamagedEvent;
		[SerializeField]
		private UnityEvent DeathEvent;

		private ZombieStateManager stateMachine;
		private int currentHealth;

		private bool noHealth { get { return currentHealth <= 0; } }

		private void Awake()
		{
			stateMachine.GetComponent<ZombieStateManager>();
			currentHealth = stats.Health;
		}
		public virtual void damage(int amount)
		{
			currentHealth -= amount;
			DamagedEvent?.Invoke();
			if (noHealth) DeathEvent?.Invoke();
		}
		public void KillZombie(float time)
		{
			Destroy(gameObject, time);
		}
	}

}

