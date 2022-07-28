using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class InteractUI : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text interactText;


		public void SetText()
		{
			interactText.text = PlayerInteractionManager.CurrentInteractable.InteractText;
		}
		public void DeleteText()
		{
			interactText.text = "";
		}

	}
}

