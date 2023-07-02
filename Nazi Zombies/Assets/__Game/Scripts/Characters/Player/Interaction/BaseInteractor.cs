using UnityEngine;
using System.Collections;

namespace Player
{
	/// <summary>
	/// Activates interactables
	/// </summary>
    public abstract class BaseInteractor : MonoBehaviour
    { 
        /// <summary>
        /// Determines whether the PlayerInteractable can be interacted with or not
        /// </summary>
        /// <param name="interact"></param>
        public delegate void Activation(PlayerInteractable interact);
        public delegate void InActivation();

        public event Activation Active;
        public event InActivation Inactive;

        /// <summary>
        /// Function for whenever a certain interactable is looked at / near by player.
        /// </summary>
        /// <param name="interact"></param>
        public void ActivateInteractable(PlayerInteractable interact)
        {
            Active?.Invoke(interact);
        }
        /// <summary>
        /// No more active interactable.
        /// </summary>
        /// <param name="interact"></param>
        public void ClearInteractable()
        {
            Inactive?.Invoke();
        }

    }

}

