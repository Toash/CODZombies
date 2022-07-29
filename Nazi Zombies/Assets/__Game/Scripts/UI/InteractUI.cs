using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class InteractUI : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text interactText;

		private void Update()
		{
			interactText.text = PlayerInteractionManager.CurrentInteractText;
		}
	}
}

