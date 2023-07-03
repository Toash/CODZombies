using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using AI.Zombie;

/// <summary>
/// Spawner service
/// </summary>
public class ZombieSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("Zombie to spawn")]
    private GameObject zombieObject;

    public int ZombiesLeftToSpawnInCurrentRound { get; private set; } = 0;

    //stats of zombies to spawn
    private int zombieSpawnHealth = 100;
    private float zombieSpawnSpeed = 2f;

    [SerializeField, Tooltip("max zombies that can exist at a time.")]
    private int maxZombies = 30;
    [SerializeField]
    private bool dontSpawnZombies;

    [PropertyOrder(-1), ShowInInspector, ReadOnly, Tooltip("all possible zombie spawn points,  automatically added in code")]
    private ZombieSpawnPoint[] spawnPoints;

    public bool noZombiesLeftToSpawn { get { return ZombiesLeftToSpawnInCurrentRound <= 0; } }

    private bool underMaxZombies { get { return ServLoc.Inst.GameManager.ZombieCount <= maxZombies; } }

    private bool readyToSpawn { get { return spawnTimer > spawnRate; } }
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
        if (noZombiesLeftToSpawn) return; //already spawned all the zombies in the current wave.
        StartSpawningZombies();
    }
    private void StartSpawningZombies()
    {
        IncreaseTimer();
        if (readyToSpawn)
        {
            //Spawn zombie at random active spawnpoint
            SpawnZombie(GetRandomActiveZombieSpawnPoint().transform.position);
            spawnTimer = 0;
        }
    }
    private void OnRoundChange(int round)
    {
        CalculateZombieStats(round);
    }
    /// <summary>
    /// Calculate zombie stats based on the round 
    /// </summary>
    private void CalculateZombieStats(int round)
    {
        switch (round)
        {
            case 1:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 100;
                break;
            case 2:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 120;
                break;
            case 3:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 150;
                break;
            case 4:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 180;
                break;
            case 5:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 220;
                break;
            case 6:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 350;
                break;
            case 7:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 420;
                break;
            case 8:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 520;
                break;
            case >= 9:
                ZombiesLeftToSpawnInCurrentRound = 10;
                zombieSpawnHealth = 700;
                break;
            default:
                Debug.LogError("Round out of bounds!");
                break;
        }
    }
    /// <summary>
    /// Gets active <see cref="ZombieSpawnPoint"/>s
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
            Debug.LogError("No active spawnpoints!");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, validSpawnPoints.Length);
        return validSpawnPoints[randomIndex];
    }

    private void SpawnZombie(Vector3 pos)
    {
        var spawnedZombie = Instantiate(zombieObject, pos, Quaternion.identity);
        ZombieStateMachine zombieScript = spawnedZombie.GetComponent<ZombieStateMachine>(); //assuming zombie script is on parent
        ZombiesLeftToSpawnInCurrentRound--;
    }

    private void IncreaseTimer()
    {
        spawnTimer += Time.deltaTime;
    }

}
