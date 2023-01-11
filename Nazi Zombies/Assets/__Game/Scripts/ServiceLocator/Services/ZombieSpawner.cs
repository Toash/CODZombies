using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Spawner service
/// </summary>
public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;

    // Global
    public int CurrentZombies { get; private set; }
    public int ZombiesToSpawn { get; private set; } 

    // Stats
    [SerializeField]
    private int maxZombies = 26; 
    [SerializeField]
    private bool dontSpawnZombies;

    [PropertyOrder(-1), ShowInInspector, ReadOnly]
    private ZombieSpawnPoint[] spawnPoints;

    public bool noZombiesLeftToSpawn { get { return ZombiesToSpawn <= 0; } }

    private bool underMaxZombies { get { return CurrentZombies <= maxZombies; } }

    private bool ready { get { return timer > 2; } }
    private float timer;
    private float spawnRate = 2;

    private void OnEnable()
    {
        ServLoc.I.Rounds.RoundChanged += OnRoundChange;
    }
    private void OnDisable()
    {
        ServLoc.I.Rounds.RoundChanged -= OnRoundChange;
    }
    private void Start()
    {
        spawnPoints = FindObjectsOfType<ZombieSpawnPoint>();
    }

    private void Update()
    {
        if (dontSpawnZombies || ServLoc.I.Rounds.RoundChanging||!underMaxZombies) return;

        IncreaseTimer();
        if (timer > spawnRate)
        {
            SpawnZombie(GetRandomZombieSpawnPoint().transform.position);
            ResetTimer();
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
    private void OnRoundChange()
    {
        CalculateZombiesToSpawn();
    }
    private void CalculateZombiesToSpawn()
    {
        var currentRound = ServLoc.I.Rounds.CurrentRound;
        switch (currentRound)
        {
            case 1:
                ZombiesToSpawn = 10; 
                break;
            case > 1:
                ZombiesToSpawn = 15;
                break;

            default:
                Debug.LogError("Round out of bounds!");
                break;
        }
        ZombiesToSpawn = 10;
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
    private ZombieSpawnPoint GetRandomZombieSpawnPoint()
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

    /// <summary>
    /// Spawns zombie at <see cref="Vector3"/>
    /// </summary>
    private void SpawnZombie(Vector3 pos)
    {
        Instantiate(zombie, pos, Quaternion.identity);
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
