using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager:MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance 
	{ 
		get 
		{ 
			return instance; 
		} 
	}

	public static int currentWave;

	public static Vector3 playerPosition;


	private static GameObject player;


	private void Awake()
	{
		DoubleCheck();
		if(GameObject.FindGameObjectWithTag("Player") != null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		else
		{
			Debug.LogError("Player tag not found!");
		}

	}

	private static void Update()
	{
		playerPosition = player.gameObject.GetComponent<Transform>().position;
	}

	private void DoubleCheck()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(this.gameObject);
		DontDestroyOnLoad(this.gameObject);
	}
}
