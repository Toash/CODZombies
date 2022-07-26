using UnityEngine;
using UnityEngine.Events;

namespace Player
{
	public class PlayerHealth : MonoBehaviour,IDamagable
    {
        public PlayerStats stats;
        [Header("Unity Events")]
        public UnityEvent DamagedEvent;
        public UnityEvent DeathEvent;

        public int CurrentHealth { get; private set; }
        //-----------HEALTH STATES---------------
        private bool lowHealth
        {
            get
            {
                if (lowHealthPercentThreshold != null)
                {
                    return ((CurrentHealth / stats.MaxHealth) * 100) >= lowHealthPercentThreshold.Value;
                }
                return false;
            }
        }
        protected bool noHealth
        {
            get
            {
                return CurrentHealth <= 0;
            }
        }
		private void Awake()
		{
            CurrentHealth = stats.MaxHealth;
		}
		private void Update()
		{
            RegenHealth();
		}

		public void damage(int amount)
        {
            CurrentHealth -= amount;
            DamagedEvent?.Invoke();
            if (this.lowHealth) LowHealthEvent?.Invoke();
            if (this.noHealth) DeathEvent?.Invoke();
        }
        [Header("Thresholds")]
        public IntVariable lowHealthPercentThreshold;
        public UnityEvent LowHealthEvent;

        private void RegenHealth()
		{
            CurrentHealth = Mathf.Clamp((int)(CurrentHealth + stats.HealthRegenRate * Time.deltaTime), 0, stats.MaxHealth);
        }

    }

}
