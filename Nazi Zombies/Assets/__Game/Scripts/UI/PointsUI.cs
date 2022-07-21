using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class PointsUI : MonoBehaviour
	{
		[SerializeField]
		private PlayerStats stats;

		private TMP_Text pointsText;

		void Awake()
		{
			pointsText = this.GetComponent<TMP_Text>();
		}

		public void Update()
		{
			pointsText.text = stats.Points.ToString();
		}
	}
}

