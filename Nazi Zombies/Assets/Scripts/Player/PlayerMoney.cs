using UnityEngine;

namespace Player
{
	public class PlayerMoney : MonoBehaviour
	{
		[Header("Variable")]
		public IntVariable Money;
		[Header("Start")]
		[SerializeField] private int startingMoney;
		private void Awake()
		{
			Money.SetValue(startingMoney);
		}
        private void Start()
        {
            
        }
    }
}

