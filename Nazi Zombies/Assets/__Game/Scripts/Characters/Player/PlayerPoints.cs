using UnityEngine;
using Sirenix.OdinInspector;

namespace Player
{
	public class PlayerPoints : MonoBehaviour
	{
		[ShowInInspector,ReadOnly]
		public static int Points;

		[SerializeField]
		private PlayerStats stats;
		private void Awake()
		{
			Points = stats.StartingPoints;
		}
    }
}

