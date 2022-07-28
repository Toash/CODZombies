using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Player
{

	//ONLY FOR WALLBUYS
	public class PlayerRaycastInteractor : MonoBehaviour
	{
        [InfoBox("This is for Wallbuys")]
        [InfoBox("Setup: Interactable Layermask, set the interactable to " +
            "isTrigger, make sure IInteractable is somewhere on the gameobject ")]

        [SerializeField] private PlayerSettings settings;
        [SerializeField] private PlayerStats stats;

        [SerializeField] private LayerMask interactMask;

        [SerializeField] private Camera cam;

        public delegate void VoidDelegate();

        public static event VoidDelegate Enter;
        public static event VoidDelegate Exit;

        private void Update()
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, stats.RaycastInteractRange, interactMask, QueryTriggerInteraction.Collide))
            {
                
            }
        }
    }
}