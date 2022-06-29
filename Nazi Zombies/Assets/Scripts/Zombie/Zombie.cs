using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ZombieMovement))]
[RequireComponent(typeof(ZombieInteractor))]
public class Zombie : MonoBehaviour,IDamagable
{
	public void damage(int amount)
	{

	}
}
