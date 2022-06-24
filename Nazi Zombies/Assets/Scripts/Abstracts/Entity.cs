using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[SerializeField] protected int health;
	[SerializeField] protected int attackDamage;
	[SerializeField] protected int moveSpeed;

	public int getMoveSpeed()
	{
		return this.moveSpeed;
	}
	public void setMoveSpeed(int moveSpeed)
	{
		this.moveSpeed = moveSpeed;
	}

	public void damage(int amount)
	{
		this.health -= amount;
		if(this.health <= 0)
		{
			die();
		}
	}
	protected abstract void die();

	public virtual void Update()
	{

	}


}
