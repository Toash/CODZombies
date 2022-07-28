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

        public delegate void Interact(IPlayerInteractable interact);

        public event Interact Enter;
        public event Interact Exit;

        private LayerMask everything;
        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit,stats.RaycastInteractRange, everything=~0, QueryTriggerInteraction.Collide))
			{
                IPlayerInteractable interactable = hit.transform.GetComponent<IPlayerInteractable>();

                if (interactable != null)
				{
                    Enter(interactable);
				}
				else
				{
                    Exit(interactable);
				}
                Exit(interactable);
			}
        }
    }
}