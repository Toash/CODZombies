using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamagable
{
	public IntVariable maxHealth;

	private int health;

	private void Awake()
	{
		health = maxHealth.Value;
	}
	public void damage(int amount)
	{
		if (health <= 0) { die(); }
	}
	private void die()
	{
		Debug.Log("player dead");
	}



}
