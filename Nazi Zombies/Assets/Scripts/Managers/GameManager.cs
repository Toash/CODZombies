using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager:MonoBehaviour
{
	//singleton
	//makes a public getter and a private setter under the hood! (Not in java)
	public static GameManager Instance { get; private set; }

	[SerializeField] private GameObject player;

	public static int currentWave;
	public static Vector3 playerPosition;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		playerPosition = player.transform.position;
	}
}
