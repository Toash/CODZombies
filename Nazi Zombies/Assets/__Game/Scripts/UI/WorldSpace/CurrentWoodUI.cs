using UnityEngine;
using TMPro;

public class CurrentWoodUI : MonoBehaviour
{
	public TMP_Text currentWoodDisplay;
	public Barricade barricade;

	private void Update()
	{
		currentWoodDisplay.text = barricade.CurrentWood.ToString();
	}
}
