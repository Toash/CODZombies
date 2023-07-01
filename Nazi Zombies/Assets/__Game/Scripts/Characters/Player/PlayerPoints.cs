using UnityEngine;
using Sirenix.OdinInspector;

namespace Player
{
	public class PlayerPoints : MonoBehaviour
	{
		[ShowInInspector, ReadOnly]
		public int Points {  get; private set; }

		[SerializeField]
		private PlayerStats stats;

		private void Awake()
		{
			Points = stats.StartingPoints;
		}

		//Returns true if valid amount
        public bool Purchase(int amount)
        {
			if (Points < amount) return false;

			//Transaction successful
			Points -= amount;
			return true;
        }
		public void Receive(int amount)
        {
			Points += amount;
        }
    }
}

