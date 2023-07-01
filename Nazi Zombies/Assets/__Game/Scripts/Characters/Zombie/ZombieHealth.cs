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

		private bool died = false;// dont want to keep emitting no health event after dying

        private void Awake()
        {
			currentHealth = StartingHealth;
        }
		
		public virtual void Damage(int amount)
		{
			if(currentHealth > 0)
            {
				currentHealth -= amount;
				DamagedEvent?.Invoke();
			}

			if (noHealth && !died)
			{
				NoHealthEvent?.Invoke();
				died = true;
			}

		}
	}

}

