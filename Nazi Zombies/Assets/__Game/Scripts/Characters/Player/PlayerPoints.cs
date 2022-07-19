using UnityEngine;

namespace Player
{
	public class PlayerPoints : MonoBehaviour
	{
		[Header("ScriptableObject References")]
		public IntVariable PointsSO;
		[Header("Start")]
		[SerializeField] private int startingMoney;
		private void Awake()
		{
			PointsSO.SetValue(startingMoney);
		}
    }
}

