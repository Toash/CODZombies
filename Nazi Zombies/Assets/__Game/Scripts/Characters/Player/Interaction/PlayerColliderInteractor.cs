using System.Collections;
using UnityEngine;

namespace Player
{
	//for everything except gun wallbuys 
	public class PlayerColliderInteractor : MonoBehaviour
	{
		[SerializeField]
		private PlayerSettings settings;
		public delegate void VoidDelegate();

		public static event VoidDelegate Enter;
		public static event VoidDelegate Exit;

		//------------DEPENDENCIES---------------
		[SerializeField]
		private Rigidbody rb;
		[SerializeField]
		private Collider col;

        #region PHYSICS
        private void OnTriggerEnter(Collider other)
		{
			PlayerInteractionManager.CurrentInteractable = other.GetComponent<IPlayerInteractable>();
			
			if (PlayerInteractionManager.CurrentInteractable != null)
			{
				PlayerColliderInteractor.Enter?.Invoke();
			}
		}

		private void OnTriggerExit(Collider other)
		{ 
			if (PlayerInteractionManager.CurrentInteractable != null)
			{
				PlayerColliderInteractor.Exit?.Invoke();
				PlayerInteractionManager.CurrentInteractable = null;
			}
		}
        #endregion
    }
}
