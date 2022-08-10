using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Services are in the children of this gameObject
public class ServiceLocator : MonoBehaviour
{
	public static ServiceLocator Instance { get; private set; }

	public SBallistics Ballistics { get; private set; }
	public SGameAssets GameAssets { get; private set; }
	public SDifficulty Difficulty { get; private set; }
	public SSpawner Spawner { get; private set; }
	public SRounds Rounds { get; private set; }

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogError("Multiple Service Locators!");
			Destroy(this.gameObject);
		}
		References();
	}
	private void References()
	{
		Ballistics = GetComponentInChildren<SBallistics>();
		GameAssets = GetComponentInChildren<SGameAssets>();
		Difficulty = GetComponentInChildren<SDifficulty>();
		Spawner = GetComponentInChildren<SSpawner>();
		Rounds = GetComponentInChildren<SRounds>();
	}

}
