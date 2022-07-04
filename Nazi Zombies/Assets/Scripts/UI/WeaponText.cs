using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class WeaponText : MonoBehaviour
	{
		[SerializeField]
		private StringVariable equippedWeaponSO; //scriptable object
		[SerializeField]
		private TMP_Text weaponText;

		private void Update()
		{
			weaponText.text = equippedWeaponSO.Value;
		}
	}
}

