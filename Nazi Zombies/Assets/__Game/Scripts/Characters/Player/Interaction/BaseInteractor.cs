using UnityEngine;
using System.Collections;

namespace Player
{
    /// <summary>
    /// When the Player needs to interact with the a
    /// <see cref="PlayerInteractable"/> in some way, that way inherits from this class
    /// </summary>
    public abstract class BaseInteractor : MonoBehaviour
    { 
        /// <summary>
        /// Determines whether the PlayerInteractable can be interacted with or not
        /// </summary>
        /// <param name="interact"></param>
        public delegate void Activation(PlayerInteractable interact);

        public event Activation Active;
        public event Activation Inactive;


    }

}

