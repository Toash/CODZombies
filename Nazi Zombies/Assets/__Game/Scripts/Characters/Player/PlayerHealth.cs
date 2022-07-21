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

        protected int currentHealth;

        protected bool noHealth
        {
            get
            {
                return currentHealth <= 0;
            }
        }

        public void damage(int amount)
        {
            currentHealth -= amount;
            DamagedEvent?.Invoke();
            if (this.lowHealth) LowHealthEvent?.Invoke();
            if (this.noHealth) DeathEvent?.Invoke();
        }
        [SerializeField]
        private PlayerStats info;
        [Header("Thresholds")]
        public IntVariable lowHealthPercentThreshold;
        public UnityEvent LowHealthEvent;
        private bool lowHealth
        {
            get
            {
                if (lowHealthPercentThreshold != null)
                {
                    return ((currentHealth / stats.Health) * 100) >= lowHealthPercentThreshold.Value;
                }
                return false;
            }
        }
    }

}
