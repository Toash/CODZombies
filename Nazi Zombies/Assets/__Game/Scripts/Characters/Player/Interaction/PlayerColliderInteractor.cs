using System.Collections;
using UnityEngine;

namespace Player
{
	//for everything except gun wallbuys 
	public class PlayerColliderInteractor : MonoBehaviour
	{
		//------------DEPENDENCIES---------------
		[SerializeField] private Rigidbody rb;
		[SerializeField] private Collider col;


		public delegate void Interact(PlayerInteractable interact);

		public event Interact InsideInteractor;
		public event Interact OutsideInteractor;

		private bool ColliderInteractableExists(PlayerInteractable interactable)
		{
			return (interactable!=null)&&interactable.detectionType==PlayerInteractable.DetectionType.Collider;
		}

		private void OnTriggerStay(Collider other)
		{
			PlayerInteractable interactable = other.transform.GetComponent<PlayerInteractable>();
			if (ColliderInteractableExists(interactable))
			{
				Debug.Log("Collide");
				InsideInteractor.Invoke(interactable);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			PlayerInteractable interactable = other.transform.GetComponent<PlayerInteractable>();
			if (ColliderInteractableExists(interactable))
			{
				OutsideInteractor.Invoke(interactable);
			}
		}

	}
}
