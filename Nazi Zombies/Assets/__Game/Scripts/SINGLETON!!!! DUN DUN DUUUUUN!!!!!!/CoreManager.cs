using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : MonoBehaviour
{
	private static CoreManager _instance;
	public static CoreManager Instance { get { return _instance; } }
	private void Awake()
	{
		if (_instance == null) _instance = this;
		else Destroy(this);
	}

}
