using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Player
{
	//takes care of interacting, listening to interaction events
	public class PlayerInteractionManager : MonoBehaviour
    {
		//testing
		[SerializeField] private PlayerStats stats;
		//[SerializeField] private PlayerColliderInteractor colInteract;
		//[SerializeField] private PlayerRaycastInteractor rayInteract;

		public static IPlayerInteractable CurrentInteractable;


        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            
        }
        private void Update()
		{

		}


		private void Interact()
		{
			if (CurrentInteractable==null) return;
			CurrentInteractable.PlayerInteract();
		}
	}
}

