using UnityEngine;

namespace Player
{
	public class PlayerMoney : MonoBehaviour
	{
		[SerializeField] private int money;

		public void ApplyChange(int amount)
		{
			this.money += amount;
		}

	}
}

