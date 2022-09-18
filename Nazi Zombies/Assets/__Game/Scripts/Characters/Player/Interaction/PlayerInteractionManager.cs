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

		[SerializeField] private PlayerStats stats;
		[SerializeField] private PlayerSettings settings;

		
		[SerializeField] private List<BaseInteractor> interators;

		public static PlayerInteractable CurrentInteractable { get; private set; }

		private bool InteractableExists { get { return CurrentInteractable != null; } }

        private void OnEnable()
        {
			foreach(BaseInteractor interactor in this.interators)
            {
				interactor.Active += SetInteractable;
				interactor.Inactive += SetInteractable;
            }
        }
        private void OnDisable()
        {
            foreach (BaseInteractor interactor in this.interators)
            {
                interactor.Active -= SetInteractable;
                interactor.Inactive -= SetInteractable;
            }
        }
        private void Update()
		{
			if (InteractableExists)
			{
				if (Input.GetKeyDown(settings.Interact))
				{
					CurrentInteractable.Interact();
				}
			}
		}
		private void SetInteractable(PlayerInteractable interact)
		{
			PlayerInteractionManager.CurrentInteractable = interact;
		}
		private void ClearInteractable(PlayerInteractable interact)
		{
			PlayerInteractionManager.CurrentInteractable = null;
		}
	}
}

