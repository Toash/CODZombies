using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Services are in the children of this gameObject
public class ServiceLocator : MonoBehaviour
{
	public static ServiceLocator Instance { get; private set; }

	public Ballistics Ballistics { get; private set; }
	public GameAssets GameAssets { get; private set; }
	public Difficulty Difficulty { get; private set; }
	public ZombieSpawner Spawner { get; private set; }
	public Rounds Rounds { get; private set; }

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
		Ballistics = GetComponentInChildren<Ballistics>();
		GameAssets = GetComponentInChildren<GameAssets>();
		Difficulty = GetComponentInChildren<Difficulty>();
		Spawner = GetComponentInChildren<ZombieSpawner>();
		Rounds = GetComponentInChildren<Rounds>();
	}

}
