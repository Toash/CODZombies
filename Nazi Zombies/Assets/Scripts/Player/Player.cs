using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCamera))]
public class Player : Entity
{
	[SerializeField] private int money;

	protected override void die()
	{
		Debug.Log("Player dead");
	}
	protected virtual void move()
	{

	}


}
