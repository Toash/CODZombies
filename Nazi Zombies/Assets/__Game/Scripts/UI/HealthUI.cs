using UnityEngine;
using UnityEngine.UI;

namespace Player.UI
{
	public class HealthUI : MonoBehaviour
	{
		public Slider bar;
		public PlayerStats stats;

		private void Start()
		{
			UpdateHealthBar();
		}
		public void UpdateHealthBar()
		{
			PlayerHealth playerHealth = PlayerRef.Instance.GetComponent<PlayerHealth>();
			int currenthealth = playerHealth.CurrentHealth;
			if (playerHealth != null)
			{
				bar.value = Mathf.InverseLerp(0,stats.MaxHealth,currenthealth);
			}
			else
			{
				Debug.LogError("Player does not have a PlayerHealth Component");
			}
		}
	}
}