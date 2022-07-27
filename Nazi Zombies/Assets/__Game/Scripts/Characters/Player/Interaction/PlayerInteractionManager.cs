using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Player
{
	public class PlayerInteractionManager : MonoBehaviour
    {
		
		[SerializeField]
		private PlayerStats stats;


		public delegate void VoidDelegate();

		public static event VoidDelegate InteractEnterEvent;
		public static event VoidDelegate InteractExitEvent;

		// The current interactable
		protected IPlayerInteractable currentInteractable;


		public IPlayerInteractable CurrentInteractable { get { return currentInteractable; } }

		protected bool canInteract { get; private set; }

		private void Update()
		{

		}


		private void Interact()
		{
			if (!canInteract||currentInteractable==null) return;
			currentInteractable.Interact();
		}
	}
}

