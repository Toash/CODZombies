using UnityEngine;
using UnityEngine.Events;
namespace Player
{
    public class PlayerInteractor : MonoBehaviour
    {
		[SerializeField]
		private PlayerStats stats;
		[SerializeField]
		private SphereCollider interactCollider;
		[SerializeField]
		private UnityEvent enterEvent;
		[SerializeField]
		private UnityEvent exitEvent;

		// The current interactable
		private IPlayerInteractable currentInteractable;

		private Collider currentCollider;

		public delegate void InteractDelegate();
		public event InteractDelegate EnteredInteractable;
		public event InteractDelegate ExitInteractable;

		private bool canInteract;

		public IPlayerInteractable CurrentInteractable { get { return currentInteractable; } }

		private void Awake()
		{
			interactCollider.radius = stats.InteractRange;
		}
		private void OnTriggerEnter(Collider other)
		{
			currentInteractable = other.GetComponent<IPlayerInteractable>();
			if (currentInteractable != null)
			{
				enterEvent?.Invoke();
			}
		}

		private void OnTriggerStay(Collider other)
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

