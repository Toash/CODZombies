using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public static SpawnManager Instance { get; private set; }
	private void Awake()
	{
		Instance = this;
	}
}
