using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ZombieStats : ScriptableObject
{
	//movement
	public float Speed;
	//health
	public int Health;

	//combat
	public int Damage;
	public float AttackSpeed;
	public float BarricadeBreakSpeed;
}
