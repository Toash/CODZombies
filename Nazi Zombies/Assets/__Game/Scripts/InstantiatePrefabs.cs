using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


//Handles instantiating prefabs at the start
public class InstantiatePrefabs : MonoBehaviour
{
	[InfoBox("Handles instantiating prefabs at the start")]
	[SerializeField]
	private GameObject EventSystemPrefab;

	[SerializeField]
	private GameObject UIPrefab;

	[SerializeField]
	private GameObject PlayerPrefab;



	private void Initialize()
	{
		Instantiate(EventSystemPrefab,null);
		Instantiate(UIPrefab, null);
		GameObject player = Instantiate(PlayerPrefab,null);
		StartPoint start = FindObjectOfType<StartPoint>();
		if (start != null)
		{
			player.transform.position = start.transform.position;
			Quaternion yRot = Quaternion.Euler(0, start.transform.rotation.eulerAngles.y, 0);
			player.transform.rotation = yRot;
		}
		else
		{

			Debug.LogWarning("Player start location not found, placing player at origin");
			player.transform.position = Vector3.zero;
			player.transform.rotation = Quaternion.identity;

		}
		//check if in main menu scene

	}
}
