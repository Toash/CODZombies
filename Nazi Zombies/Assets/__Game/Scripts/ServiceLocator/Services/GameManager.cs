using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	//waves
	public static int CurrentRound;
	private int maxZombies;
	private static int currentZombies;
	public delegate void Action();
	public static Action roundChanged;
	//zones
	//spawners
	private ZombieSpawnPoint[] spawnPoints;

    private void Start()
    {
		spawnPoints = FindObjectsOfType<ZombieSpawnPoint>();
    }



    public static void IncreaseRound()
    {
		CurrentRound += 1;
		roundChanged?.Invoke();
    }
}
