using System.Collections;
using UnityEngine;


public abstract class Interactable : MonoBehaviour
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
