using System.Collections;
using UnityEngine;

namespace Player
{
	//for everything except gun wallbuys 
	public class PlayerColliderInteractor : BaseInteractor
	{
		//------------DEPENDENCIES---------------
		[SerializeField] private Rigidbody rb;
		[SerializeField] private Collider col;

		private bool ColliderInteractableExists(PlayerInteractable interactable)
		{
			return (interactable!=null)&&interactable.DetectType==PlayerInteractable.DetectionType.Collider;
		}

		private void OnTriggerEnter(Collider other)
		{
			PlayerInteractable interactable = other.transform.GetComponent<PlayerInteractable>();
			if (ColliderInteractableExists(interactable))
			{
				//Debug.Log("Collide");
				base.ActivateInteractable(interactable);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			PlayerInteractable interactable = other.transform.GetComponent<PlayerInteractable>();
			if (ColliderInteractableExists(interactable))
			{
				if(ColliderInteractableExists(PlayerInteractionManager.CurrentInteractable)){
                    //Debug.Log("Exiting");
                    base.ClearInteractable();
                }
			}
		}

	}
}
