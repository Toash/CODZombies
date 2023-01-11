using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Services are in the children of this gameObject
public class ServLoc : MonoBehaviour
{
	public static ServLoc I { get; private set; }

	public Ballistics Ballistics { get; private set; }
	public ZombieSpawner Spawner { get; private set; }
	public Rounds Rounds { get; private set; }

	private void Awake()
	{
		if(I == null)
		{
			I = this;
		}
		else
		{
			Debug.LogError("Multiple Service Locators!");
			Destroy(this.gameObject);
		}
        Ballistics = GetComponentInChildren<Ballistics>();
        Spawner = GetComponentInChildren<ZombieSpawner>();
        Rounds = GetComponentInChildren<Rounds>();
    }
}
