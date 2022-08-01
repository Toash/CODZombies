using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles rounds, spawning zombies, difficulty. 
/// </summary>
public class GameManager : MonoBehaviour
{

	[SerializeField]
	private int maxZombies;
	[SerializeField]
	private float zombieSpawnFrequency;

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
		IncreaseTimer();
        if(!roundChanging)
        {
            if (underMaxZombies&&GetValidSpawnPoints().Length>0)
            {
                if (timer >= zombieSpawnFrequency)
                {
					SpawnZombie();
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

	private void SpawnZombie()
    {
		if (GetValidSpawnPoints().Length==0)
        {
			Debug.Log("Spawning zombie with no active spawnpoints");
        }
		var zombie = ServiceLocator.Instance.GameAssets.zombie;
		//Find spawn point
		ZombieSpawnPoint[] validSpawnPoints = GetValidSpawnPoints();
		
		int index = UnityEngine.Random.Range(0, validSpawnPoints.Length);

		//spawn zombie
		Instantiate(zombie, validSpawnPoints[index].transform.position, Quaternion.identity);
    }
	private ZombieSpawnPoint[] GetValidSpawnPoints()
    {
		List<ZombieSpawnPoint> validSpawnPoints = new List<ZombieSpawnPoint>();
		foreach(ZombieSpawnPoint spawn in spawnPoints)
        {
            if (spawn.Active == true)
            {
				validSpawnPoints.Add(spawn);
            }
        }
		return validSpawnPoints.ToArray();
    }
	private bool AtLeastOneSpawnPointActivate(ZombieSpawnPoint[] spawnPoints)
    {
		foreach(ZombieSpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.Active == true)
            {
				return true;
            }
        }
		return false;

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
