using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class PointsUI : MonoBehaviour
	{
		[SerializeField]
		private PlayerStats stats;

		private TMP_Text pointsText;
		
		private PlayerRef playerRef;
		private PlayerPoints playerPoints;

		void Awake()
		{
			pointsText = this.GetComponent<TMP_Text>();
		}
		
		void Start()
		{
			playerRef = PlayerRef.Instance.GetComponent<PlayerRef>();
			playerPoints = playerRef.GetComponent<PlayerPoints>();
		}

		public void Update()
		{
			pointsText.text = playerPoints.Points.ToString();
		}
	}
}

