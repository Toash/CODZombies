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
			bar.value = Mathf.InverseLerp(0,stats.MaxHealth,PlayerHealth.CurrentPlayerHealth);
		}
	}
}