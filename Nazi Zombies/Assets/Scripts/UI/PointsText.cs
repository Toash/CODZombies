using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class PointsText : MonoBehaviour
	{
		[SerializeField]
		private IntVariable points;

		private TMP_Text pointsText;

		void Awake()
		{
			pointsText = this.GetComponent<TMP_Text>();
		}

		public void UpdateText()
		{
			pointsText.text = points.Value.ToString();
		}
	}
}

