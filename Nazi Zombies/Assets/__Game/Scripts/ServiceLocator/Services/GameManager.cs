using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/// <summary>
/// Handles rounds, spawning zombies, difficulty. 
/// </summary>
public class GameManager : MonoBehaviour
{
	[SerializeField]
	private int maxZombies;
	[SerializeField]
	private float zombieSpawnFrequency;
	[SerializeField, Title("Debug")]
	private bool spawnZombies;

	public int CurrentRound { get; private set; }
	public int CurrentZombies { get; private set; }

	public delegate void Action();
	public Action roundChanged;

	private ZombieSpawnPoint[] spawnPoints;

	private bool roundChanging;
	private bool noZombies { get { return CurrentZombies<=0; } }
	private bool underMaxZombies { get { return CurrentZombies <= maxZombies; } }

	private float timer;



	private void Start()
    {
		spawnPoints = FindObjectsOfType<ZombieSpawnPoint>();
    }
    private void Update()
    {
		if (!spawnZombies) return;
		IncreaseTimer();
        if(!roundChanging)
        {
            if (underMaxZombies)
            {
                if (timer > zombieSpawnFrequency)
                {
					SpawnZombieAtActiveSpawnPoint();
					ResetTimer();
                }
            }
        }

    }
	public void AddZombieCount()
    {
		CurrentZombies += 1;
    }
	public void RemoveZombieCount()
    {
		CurrentZombies -= 1;
    }


    private void IncreaseRound()
    {
		CurrentRound += 1;
		roundChanged?.Invoke();
    }

	private void SpawnZombieAtActiveSpawnPoint()
    {
		var zombie = ServiceLocator.Instance.GameAssets.zombie;
		ZombieSpawnPoint[] validSpawnPoints = GetActiveSpawnPoints();
		if (validSpawnPoints.Length==0)
        {
			Debug.LogError("Spawning zombie with no active spawnpoints");
			return;
        }

		int index = UnityEngine.Random.Range(0, validSpawnPoints.Length);

		Instantiate(zombie, validSpawnPoints[index].transform.position, Quaternion.identity);
    }
	private ZombieSpawnPoint[] GetActiveSpawnPoints()
    {
		List<ZombieSpawnPoint> activeSpawnPoints = new List<ZombieSpawnPoint>();
		foreach(ZombieSpawnPoint spawn in spawnPoints)
        {
            if (spawn.Active == true)
            {
				activeSpawnPoints.Add(spawn);
            }
        }
		return activeSpawnPoints.ToArray();
    }
	private void IncreaseTimer()
	{
		timer += Time.deltaTime;
	}
	private void ResetTimer()
	{
		timer = 0;
	}

}
