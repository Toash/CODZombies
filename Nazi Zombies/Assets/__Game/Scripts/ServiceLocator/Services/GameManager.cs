using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Handles rounds, spawning, and zones. 
/// </summary>
public class GameManager : MonoBehaviour
{
	//rounds
	public static int CurrentRound;
	private int maxZombies;
	private static int currentZombies;
	public delegate void Action();
	public static Action roundChanged;

	//spawners 
	private ZombieSpawnPoint[] spawnPoints;


	//zones
	

	private void Start()
    {
		spawnPoints = FindObjectsOfType<ZombieSpawnPoint>();
    }
    private void Update()
    {
        
    }

    //rounds
    private static void IncreaseRound()
    {
		CurrentRound += 1;
		roundChanged?.Invoke();
    }
	//spawners
	private void SpawnZombie()
    {
		var zombie = ServiceLocator.Instance.GameAssets.zombie;

		//Find spawn point
		ZombieSpawnPoint[] valid = GetValidSpawnPoints();
		int index = Random.Range(0, valid.Length);

		//spawn zombie
		Instantiate(zombie, valid[index].transform.position, Quaternion.identity);
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

	//zones

}
