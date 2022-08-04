using UnityEngine;
using UnityEngine.Events;

namespace AI.Zombie
{
	public class ZombieHealth : MonoBehaviour, IDamagable
	{
		public int StartingHealth = 100;

		[SerializeField]
		private UnityEvent DamagedEvent;
		[SerializeField]
		private UnityEvent NoHealthEvent;

		private int currentHealth;

		private bool noHealth { get { return currentHealth <= 0; } }

        private void OnEnable()
        {
			ServiceLocator.Instance.GameManager.AddZombieCount();
        }
        private void OnDisable()
        {
			ServiceLocator.Instance.GameManager.RemoveZombieCount();
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

