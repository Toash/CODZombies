using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
namespace Player
{
	public class PlayerBaseInteractor : MonoBehaviour
    {
		
		[SerializeField]
		private PlayerStats stats;

		[SerializeField]
		private UnityEvent enterEvent;
		[SerializeField]
		private UnityEvent exitEvent;

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

