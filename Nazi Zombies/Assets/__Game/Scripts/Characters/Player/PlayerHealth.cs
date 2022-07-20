using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerHealth : BaseHealth,IDamagable
    {
        [SerializeField]
        private PlayerInfo info;
        [Header("Thresholds")]
        public IntVariable lowHealthPercentThreshold;
        public UnityEvent LowHealthEvent;
        private bool lowHealth
        {
            get
            {
                if (lowHealthPercentThreshold != null)
                {
                    return ((currentHealth / startingHealth.Value) * 100) >= lowHealthPercentThreshold.Value;
                }
                return false;
            }
        }

        public override void damage(int amount)
		{
            base.damage(amount);
            if (this.lowHealth)
            {
                LowHealthEvent?.Invoke();
            }
        }
    }

}