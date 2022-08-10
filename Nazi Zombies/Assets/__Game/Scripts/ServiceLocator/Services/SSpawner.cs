using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Spawner service
/// </summary>
public class SSpawner : MonoBehaviour
{
    // Dependencies
    [SerializeField]
    private SRounds roundManager;


    // Global
    public int CurrentZombies { get; private set; }

    // Stats
    [SerializeField]
    private int maxZombies = 26; //Maximum zombie that can exist at a time
    [SerializeField]
    private bool dontSpawnZombies;
    private readonly float SPAWN_RATE = 2;
    private int zombiesToSpawn; //Zombies to spawn in the current round

    [PropertyOrder(-1), ShowInInspector, ReadOnly]
    private ZombieSpawnPoint[] spawnPoints;

    private bool underMaxZombies { get { return CurrentZombies <= maxZombies; } }
    private float timer;

    private void Start()
    {
        spawnPoints = FindObjectsOfType<ZombieSpawnPoint>();
    }

    private void Update()
    {
        if (dontSpawnZombies) return;

        IncreaseTimer();
        if (!roundManager.RoundChanging)
        {
            if (underMaxZombies)
            {
                if (timer > SPAWN_RATE)
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
    private void CalculateZombiesToSpawn()
    {

    }

    private void SpawnZombieAtActiveSpawnPoint()
    {
        var zombie = ServiceLocator.Instance.GameAssets.zombie;
        ZombieSpawnPoint[] validSpawnPoints = GetActiveSpawnPoints();
        if (validSpawnPoints.Length == 0)
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
        foreach (ZombieSpawnPoint spawn in spawnPoints)
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
