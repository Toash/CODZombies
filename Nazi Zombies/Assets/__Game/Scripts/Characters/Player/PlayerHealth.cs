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

        public static int CurrentPlayerHealth;
        //-----------HEALTH STATES---------------
        private bool lowHealth
        {
            get
            {
                if (lowHealthPercentThreshold != null)
                {
                    return ((CurrentPlayerHealth / stats.MaxHealth) * 100) >= lowHealthPercentThreshold.Value;
                }
                return false;
            }
        }
        protected bool noHealth
        {
            get
            {
                return CurrentPlayerHealth <= 0;
            }
        }
		private void Awake()
		{
            CurrentPlayerHealth = stats.MaxHealth;
		}
		private void Update()
		{
            RegenHealth();
		}

		public void damage(int amount)
        {
            CurrentPlayerHealth -= amount;
            DamagedEvent?.Invoke();
            if (this.lowHealth) LowHealthEvent?.Invoke();
            if (this.noHealth) DeathEvent?.Invoke();
        }
        [Header("Thresholds")]
        public IntVariable lowHealthPercentThreshold;
        public UnityEvent LowHealthEvent;

        private void RegenHealth()
		{
            CurrentPlayerHealth = Mathf.Clamp((int)(CurrentPlayerHealth + stats.HealthRegenRate * Time.deltaTime), 0, stats.MaxHealth);
        }

    }

}
