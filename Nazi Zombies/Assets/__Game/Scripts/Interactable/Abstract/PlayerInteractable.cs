using System.Collections;
using UnityEngine;

/// <summary>
/// Can be interacted by player
/// </summary>
public abstract class PlayerInteractable : MonoBehaviour
{

	public enum InteractionType
	{
		OneClick,
		Hold
	}
	public enum DetectionType
	{
		Raycast,
		Collider
	}
	public InteractionType interactType;
	public DetectionType detectionType;

	public abstract string GetInteractString();
	public abstract void Interact();

}
