using UnityEngine;

namespace Player
{
	public class PlayerPoints : MonoBehaviour
	{
		public static int CurrentMoney;
		[Header("Start")]
		[SerializeField] private int startingMoney;
		private void Awake()
		{
			CurrentMoney = startingMoney;
		}
    }
}

