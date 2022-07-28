using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Player
{
	//takes care of interacting, listening to interaction events
	public class PlayerInteractionManager : MonoBehaviour
    {
		//testing
		[SerializeField] private PlayerStats stats;
		[SerializeField] private PlayerSettings settings;
		[SerializeField] private PlayerColliderInteractor colInteract;
		[SerializeField] private PlayerRaycastInteractor rayInteract;

		public IPlayerInteractable CurrentInteractable { get; private set; }
		public static string CurrentInteractText;


        private void OnEnable()
        {
			colInteract.Enter += SetInteractable;
			colInteract.Exit += ClearInteractable;

			rayInteract.Enter += SetInteractable;
			rayInteract.Exit += ClearInteractable;
        }
        private void OnDisable()
        {
			colInteract.Enter -= SetInteractable;
			colInteract.Exit -= ClearInteractable;

			rayInteract.Enter -= SetInteractable;
			rayInteract.Exit -= ClearInteractable;
		}
        private void Update()
		{
			if (CurrentInteractable != null)
			{
				if (Input.GetKeyDown(settings.Interact))
				{
					CurrentInteractable.PlayerInteract();
				}
			}
		}
		private void SetInteractable(IPlayerInteractable interact)
		{
			this.CurrentInteractable = interact;
			CurrentInteractText = interact.GetInteractText();
		}
		private void ClearInteractable(IPlayerInteractable interact)
		{
			this.CurrentInteractable = null;
			CurrentInteractText = "";
		}
	}
}

