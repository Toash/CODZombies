using UnityEngine;

public class Doorbuy : PlayerInteractable
{
	[SerializeField]
	private int cost;
	[SerializeField]
	private string textToDisplay;

	private void Awake()
	{ 

	}

	public override void Interact()
	{
		base.Interact();
		Destroy(gameObject);
	}
}
