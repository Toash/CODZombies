using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class PointsUI : MonoBehaviour
	{
		[SerializeField]
		private IntVariable points;

		private TMP_Text pointsText;

		void Awake()
		{
			pointsText = this.GetComponent<TMP_Text>();
		}

		public void Update()
		{
			pointsText.text = points.Value.ToString();
		}
	}
}

