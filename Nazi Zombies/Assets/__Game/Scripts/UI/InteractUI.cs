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
            if (PlayerInteractionManager.CurrentInteractable != null)
            {
                interactText.text = PlayerInteractionManager.CurrentInteractable.InteractString;
            }
            else
            {
				interactText.text = System.String.Empty;
            }
			
		}
	}
}

