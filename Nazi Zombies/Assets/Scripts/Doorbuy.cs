using UnityEngine;

public class Doorbuy : MonoBehaviour,IPlayerInteractable
{
	[SerializeField]
	private IntVariable cost;

	public string InteractText { get; set; }
	public int Cost { get { return this.cost.Value; } }

	private void Awake()
	{
		InteractText = "Press E to buy door: cost " + cost.Value.ToString();
	}

	public void Interact()
	{
		Destroy(this.gameObject);
	}

}
