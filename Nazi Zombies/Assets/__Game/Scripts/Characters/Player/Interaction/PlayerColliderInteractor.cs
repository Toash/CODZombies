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


		public delegate void Interact(IPlayerInteractable interact);

		public event Interact Enter;
		public event Interact Exit;

		private void OnTriggerEnter(Collider other)
		{
			IPlayerInteractable interactable = other.transform.GetComponent<IPlayerInteractable>();
			if (interactable != null)
			{
				Enter.Invoke(interactable);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			IPlayerInteractable interactable = other.transform.GetComponent<IPlayerInteractable>();
			if (interactable != null)
			{
				Exit.Invoke(interactable);
			}
		}

	}
}
