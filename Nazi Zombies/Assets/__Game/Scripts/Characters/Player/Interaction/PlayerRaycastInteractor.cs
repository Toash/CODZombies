using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Player
{
    /// <summary>
    /// Uses raycast for interactions.
    /// </summary>
	public class PlayerRaycastInteractor : BaseInteractor 
	{
        [SerializeField] private PlayerStats stats;
        [SerializeField] private Camera cam;
        [SerializeField] private LayerMask mask;


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
                    base.FActive(interactable);
				}
			}
            if (RaycastInteractableExists(PlayerInteractionManager.CurrentInteractable))
            {
                base.FInActive(null);
            }
        }
    }
}