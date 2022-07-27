using System.Collections;
using UnityEngine;

namespace Player
{
	//for repairing
	public class PlayerRepairableColliderInteractor : MonoBehaviour
	{
		[SerializeField]
		private PlayerSettings settings;
		public delegate void VoidDelegate();

		public static event VoidDelegate InteractColliderEnterEvent;
		public static event VoidDelegate InteractColliderExitEvent;
		[SerializeField]
		private Rigidbody rb;
		[SerializeField]
		private Collider col;
		private void OnTriggerEnter(Collider other)
		{
			PlayerInteractionManager.currentInteractable = other.GetComponent<IPlayerInteractable>();
			if (PlayerInteractionManager.currentInteractable != null)
			{
				InteractColliderEnterEvent?.Invoke();
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (PlayerInteractionManager.currentInteractable != null) PlayerInteractionManager.CanInteract = true;
		}
		private void OnTriggerExit(Collider other)
		{
			//if exiting, not in range of interact trigger. 
			if (PlayerInteractionManager.currentInteractable != null)
			{
				InteractColliderExitEvent?.Invoke();
				PlayerInteractionManager.currentInteractable = null;
				PlayerInteractionManager.CanInteract = false;
			}
		}
	}
}
