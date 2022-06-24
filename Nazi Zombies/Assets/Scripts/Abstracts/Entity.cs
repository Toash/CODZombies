using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactor))]
public abstract class Entity : MonoBehaviour
{
	[SerializeField] protected int health;
	[SerializeField] protected int attackDamage;
	[SerializeField] protected int moveSpeed;
	[SerializeField] protected int interactRange;

	protected Interactor interactor;

	public int getInteractRange()
	{
		return interactRange;
	}

	protected virtual void Update()
	{

	}

	private void CreateInteractSphere()
	{
		//m,ake a sphere collider COMPONENT
		this.gameObject.AddComponent<SphereCollider>();
	}
	void Awake()
	{
		interactor = GetComponent<Interactor>();
	}

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
}
