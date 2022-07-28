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


		public delegate void Interact(Interactable interact);

		public event Interact InsideInteractor;
		public event Interact OutsideInteractor;

		private bool ColliderInteractableExists(Interactable interactable)
		{
			return (interactable!=null)&&interactable.detectionType==Interactable.DetectionType.Collider;
		}

		private void OnTriggerStay(Collider other)
		{
			Interactable interactable = other.transform.GetComponent<Interactable>();
			if (ColliderInteractableExists(interactable))
			{
				Debug.Log("Collide");
				InsideInteractor.Invoke(interactable);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			Interactable interactable = other.transform.GetComponent<Interactable>();
			if (ColliderInteractableExists(interactable))
			{
				OutsideInteractor.Invoke(interactable);
			}
		}

	}
}
