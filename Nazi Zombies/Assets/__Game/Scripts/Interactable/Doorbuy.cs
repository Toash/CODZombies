using UnityEngine;

public class Doorbuy : Interactable
{
	[SerializeField]
	private int cost;
	[SerializeField]
	private string textToDisplay;

	private void Awake()
	{ 

	}

	public override string GetInteractString()
	{
		return textToDisplay;
	}

	public override void Interact()
	{
		Destroy(gameObject);
	}
}
