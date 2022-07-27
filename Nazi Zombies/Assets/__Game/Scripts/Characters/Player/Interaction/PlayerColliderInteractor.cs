using System.Collections;
using UnityEngine;

namespace Player
{
	//for repairing
	public class PlayerRepairableColliderInteractor : PlayerBaseInteractor
	{
		[SerializeField]
		private Rigidbody rb;
		[SerializeField]
		private Collider col;
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
			if (currentInteractable != null) canInteract = true;
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
	}
}
