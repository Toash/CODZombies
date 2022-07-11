using UnityEngine;
using TMPro;

namespace Player.UI
{
	public class InteractUI : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text interactText;

		private PlayerInteractor interactor;

		private void Start()
		{
			interactor = FindObjectOfType<PlayerInteractor>();
		}

		public void SetText()
		{
			interactText.text = interactor.CurrentInteractable.InteractText;
		}
		public void DeleteText()
		{
			interactText.text = "";
		}

	}
}

