using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Player
{
	//takes care of interacting, listening to interaction events
	public class PlayerInteractionManager : MonoBehaviour
    {
		[SerializeField] private PlayerStats stats;
		[SerializeField] private PlayerSettings settings;

		[SerializeField] private PlayerColliderInteractor colInteracter;
		[SerializeField] private PlayerRaycastInteractor rayInteracter;

		public Interactable CurrentInteractable { get; private set; }
		public static string CurrentInteractText;

        private void OnEnable()
        {
			colInteracter.InsideInteractor += SetInteractable;
			colInteracter.OutsideInteractor += ClearInteractable;

			rayInteracter.LookingAtInteractor += SetInteractable;
			rayInteracter.LookingAwayFromInteractor += ClearInteractable;
        }
        private void OnDisable()
        {
			colInteracter.InsideInteractor -= SetInteractable;
			colInteracter.OutsideInteractor -= ClearInteractable;

			rayInteracter.LookingAtInteractor -= SetInteractable;
			rayInteracter.LookingAwayFromInteractor -= ClearInteractable;
		}
        private void Update()
		{
			if (CurrentInteractable != null)
			{
				if (Input.GetKeyDown(settings.Interact))
				{
					CurrentInteractable.Interact();
				}
			}
		}
		private void SetInteractable(Interactable interact)
		{
			this.CurrentInteractable = interact;
			CurrentInteractText = interact.GetInteractString();
		}
		private void ClearInteractable(Interactable interact)
		{
			this.CurrentInteractable = null;
			CurrentInteractText = "";
		}
	}
}

