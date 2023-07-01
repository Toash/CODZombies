using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Sirenix.OdinInspector;
namespace Player
{
    /// <summary>
    /// takes care of interacting, listening to interaction events, contains the <see cref="CurrentInteractable"/>
    /// </summary>
    public class PlayerInteractionManager : MonoBehaviour
    {
        [ShowInInspector, ReadOnly, PropertyOrder(-1)]
        public static PlayerInteractable CurrentInteractable { get; private set; }

        [SerializeField]
		private PlayerStats stats;
		[SerializeField]
		private PlayerSettings settings;

		
		//The players interactors
		[SerializeField]
		private List<BaseInteractor> playersInteractors;


		private bool InteractableExists { get { return CurrentInteractable != null; } }

        private void Awake()
        {
            foreach(BaseInteractor interactor in GetComponentsInChildren<BaseInteractor>())
			{
				playersInteractors.Add(interactor);
			}
        }

        private void OnEnable()
        {
			foreach(BaseInteractor interactor in this.playersInteractors)
            {
				interactor.Active += SetInteractable;
				interactor.Inactive += ClearInteractable;
            }
        }
        private void OnDisable()
        {
            foreach (BaseInteractor interactor in this.playersInteractors)
            {
                interactor.Active -= SetInteractable;
                interactor.Inactive -= ClearInteractable;
            }
        }
        private void Update()
		{
			if (InteractableExists)
			{
				if (Input.GetKey(settings.Interact))
				{
					CurrentInteractable.Interact();
				}
			}
		}
		private void SetInteractable(PlayerInteractable interact)
		{
			CurrentInteractable = interact;
			//print("set interactable");
		}
		private void ClearInteractable()
		{
			CurrentInteractable = null;
			print("clear interactable");
		}
	}
}

