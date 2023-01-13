using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Services are in the children of this gameObject
public class ServLoc : MonoBehaviour
{
	public static ServLoc Inst { get; private set; }

	public GameManager GameManager { get; private set; }
	public Ballistics Ballistics { get; private set; }
	public ZombieSpawner ZombieSpawner { get; private set; }
	public Rounds Rounds { get; private set; }

	private void Awake()
	{
		if(Inst == null)
		{
			Inst = this;
		}
		else
		{
			Debug.LogError("Multiple Service Locators!");
			Destroy(this.gameObject);
		}
		GameManager = GetComponentInChildren<GameManager>();
        Ballistics = GetComponentInChildren<Ballistics>();
        ZombieSpawner = GetComponentInChildren<ZombieSpawner>();
        Rounds = GetComponentInChildren<Rounds>();
    }
}
