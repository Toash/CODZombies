using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCamera))]
public class Player : MonoBehaviour, IDamagable,IKillable
{
	[SerializeField] private int health=100;
	[SerializeField] private int money;
	[SerializeField] private int playerMoveSpeed=5;
	[SerializeField] private int weaponSlots = 2;

	public int getPlayerMoveSpeed()
	{
		return playerMoveSpeed;
	}

	public void damage(int amount)
	{
		health -= amount;
		if (health <= 0) { die(); }
	}
	public void die()
	{
		Debug.Log("Player dead");
	}


}
