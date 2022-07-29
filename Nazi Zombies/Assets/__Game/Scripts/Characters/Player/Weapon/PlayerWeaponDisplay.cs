using UnityEngine;

namespace Player
{
	public class PlayerWeaponDisplay : MonoBehaviour
	{
		[SerializeField] private PlayerInventory playerInventory;

		[SerializeField] private Transform dynamicWeaponHoldPoint;

		private void Start()
		{
			cachedWeaponHoldPoint = dynamicWeaponHoldPoint.localPosition;
		}
	}
}