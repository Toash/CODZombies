using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Spawner service
/// </summary>
public class ZombieSpawner : MonoBehaviour
{

    [SerializeField, Tooltip("Zombie to spawn")] 
    private GameObject zombie;


    public int ZombiesLeftToSpawnInCurrentRound { get; private set; }

    //stats of zombies to spawn
    private int zombieSpawnHealth = 100;
    private bool zombieIsRunning = false;

    [SerializeField, Tooltip("max zombies that can exist at a time.")] 
    private int maxZombies = 26; 
    [SerializeField] 
    private bool dontSpawnZombies;

    [PropertyOrder(-1), ShowInInspector, ReadOnly, Tooltip("all possible zombie spawn points,  automatically added")]
    private ZombieSpawnPoint[] spawnPoints;

    public bool noZombiesLeftToSpawn { get { return ZombiesLeftToSpawnInCurrentRound <= 0; } }

    private bool underMaxZombies { get { return ServLoc.Inst.GameManager.CurrentZombies <= maxZombies; } }

    private bool readyToSpawn { get { return spawnTimer > 2; } }
    private float spawnTimer;
    private float spawnRate = 2;

    private void Start()
    {
        spawnPoints = FindObjectsOfType<ZombieSpawnPoint>();
        ServLoc.Inst.Rounds.RoundChanged += OnRoundChange;
    }

    private void OnDisable()
    {
        ServLoc.Inst.Rounds.RoundChanged -= OnRoundChange;
    }

    private void Update()
    {
        if (dontSpawnZombies) return;
        if (ServLoc.Inst.Rounds.RoundChanging) return; //dont spawn zombies in between rounds
        if (!underMaxZombies) return; // dont spawn zombmies when reach a certain limit
        SpawningZombies();
    }
    private void SpawningZombies()
    {
        IncreaseTimer();
        if (spawnTimer > spawnRate)
        {
            SpawnZombie(GetRandomActiveZombieSpawnPoint().transform.position);
            spawnTimer = 0;
        }
    }
    private void OnRoundChange()
    {
        CalculateSpawnedZombieStats();
    }
    private void CalculateSpawnedZombieStats()
    {
        // depends on the round
        switch (ServLoc.Inst.Rounds.CurrentRound)
        {
            case 1:
                ZombiesLeftToSpawnInCurrentRound = 10;
                
                zombieSpawnHealth = 100;
                zombieIsRunning = false;
                break;
            case 2:
                ZombiesLeftToSpawnInCurrentRound = 10;

                zombieSpawnHealth = 120;
                zombieIsRunning = false;
                break;
            case 3:
                ZombiesLeftToSpawnInCurrentRound = 10;

                zombieSpawnHealth = 150;
                zombieIsRunning = false;
                break;
            case 4:
                ZombiesLeftToSpawnInCurrentRound = 10;

                zombieSpawnHealth = 180;
                zombieIsRunning = false;
                break;
            case 5:
                ZombiesLeftToSpawnInCurrentRound = 10;

                zombieSpawnHealth = 220;
                zombieIsRunning = false;
                break;
            case 6:
                ZombiesLeftToSpawnInCurrentRound = 10;

                zombieSpawnHealth = 350;
                zombieIsRunning = false;
                break;
            case 7:
                ZombiesLeftToSpawnInCurrentRound = 10;

                zombieSpawnHealth = 420;
                zombieIsRunning = false;
                break;
            case 8:
                ZombiesLeftToSpawnInCurrentRound = 10;

                zombieSpawnHealth = 520;
                zombieIsRunning = false;
                break;
            case >= 9:
                ZombiesLeftToSpawnInCurrentRound = 10;

                zombieSpawnHealth = 700;
                zombieIsRunning = true;
                break;
            default:
                Debug.LogError("Round out of bounds!");
                break;
        }
        Debug.Log("New Round, ");
        Debug.Log($"Zombies to spawn: {ZombiesLeftToSpawnInCurrentRound}");
        Debug.Log($"Spawned Zombie health: {zombieSpawnHealth}");
        Debug.Log($"Spawned Zombie running: {zombieIsRunning}");
    }
    /// <summary>
    /// Gets actived <see cref="ZombieSpawnPoint"/>s
    /// </summary>
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

    /// <summary>
    /// Gets random zombie spawn point in an active zone
    /// </summary>
    /// <returns></returns>
    private ZombieSpawnPoint GetRandomActiveZombieSpawnPoint()
    {
        ZombieSpawnPoint[] validSpawnPoints = GetActiveSpawnPoints();
        if (validSpawnPoints.Length == 0)
        {
            Debug.LogError("Trying to spawn zombie with no active spawnpoints");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, validSpawnPoints.Length);
        return validSpawnPoints[randomIndex];
    }

    private void SpawnZombie(Vector3 pos)
    {
        var spawnedZombie = Instantiate(zombie, pos, Quaternion.identity);
    }



    private void IncreaseTimer()
    {
        spawnTimer += Time.deltaTime;
    }

}
