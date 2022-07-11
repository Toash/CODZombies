using UnityEngine;

public class Doorbuy : MonoBehaviour,IPlayerInteractable
{
	[SerializeField]
	private IntVariable cost;
	[SerializeField]
	private string textToDisplay;

	public string InteractText { get; set; }
	public int Cost { get { return this.cost.Value; } }

	private void Awake()
	{
		textToDisplay=textToDisplay.Replace("<cost>", this.cost.Value.ToString());
		InteractText = textToDisplay;
	}

	public void Interact()
	{
		Destroy(this.gameObject);
	}

}
