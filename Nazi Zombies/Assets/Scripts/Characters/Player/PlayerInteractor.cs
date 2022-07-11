using UnityEngine;
using UnityEngine.Events;
namespace Player
{
	[RequireComponent(typeof(PlayerInput))]
    public class PlayerInteractor : BaseInteractor
    {
		public UnityEvent enterEvent;
		public UnityEvent exitEvent;

		// The current interactable
		private IPlayerInteractable currentInteractable;

		public delegate void InteractDelegate();
		public event InteractDelegate EnteredInteractable;
		public event InteractDelegate ExitInteractable;

		private PlayerInput input;
		private bool canInteract;

		public IPlayerInteractable CurrentInteractable { get { return currentInteractable; } }

		private void OnEnable()
		{
			input.interactClicked += Interact;
		}
		private void OnDisable()
		{
			input.interactClicked -= Interact;
		}

		protected override void Awake()
		{
			base.Awake();
			input = this.GetComponent<PlayerInput>();
		}
		private void OnTriggerEnter(Collider other)
		{
			currentInteractable = other.GetComponent<IPlayerInteractable>();
			if (currentInteractable != null)
			{
				enterEvent?.Invoke();
			}
		}

		protected override void OnTriggerStay(Collider other)
		{
			if(currentInteractable != null) canInteract = true;
		}
		private void OnTriggerExit(Collider other)
		{
			//if exiting, not in range of interact trigger. 
			if (currentInteractable != null)
			{
				exitEvent?.Invoke();
				currentInteractable = null;
				canInteract = false;
			}
		}
		private void Interact()
		{
			if (!canInteract||currentInteractable==null) return;
			currentInteractable.Interact();
		}
	}
}

