using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Player
{

	//ONLY FOR WALLBUYS
    //this shoudl take precendent
	public class PlayerRaycastInteractor : MonoBehaviour
	{
        [SerializeField] private PlayerStats stats;
        [SerializeField] private Camera cam;
        [SerializeField] private LayerMask mask;

        public delegate void Interact(PlayerInteractable interact);

        public event Interact LookingAtInteractor;
        public event Interact LookingAwayFromInteractor;

        private bool RaycastInteractableExists(PlayerInteractable interactable)
        {
            return (interactable != null) && interactable.DetectType == PlayerInteractable.DetectionType.Raycast;
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit,stats.RaycastInteractRange, mask, QueryTriggerInteraction.Collide))
			{
                PlayerInteractable interactable = hit.transform.GetComponent<PlayerInteractable>();

                if (RaycastInteractableExists(interactable))
				{
                    LookingAtInteractor(interactable);
				}
				else if(!RaycastInteractableExists(interactable))
				{
                    LookingAwayFromInteractor(interactable);
				}
			}
        }
    }
}