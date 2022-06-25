using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour,IDamagable,IKillable
{
	[SerializeField] private int health =100;
	[SerializeField] private int zombieMoveSpeed=1;
	private NavMeshAgent agent;

	public void setZombieMoveSpeed(int zombieMoveSpeed)
	{
		this.zombieMoveSpeed = zombieMoveSpeed;
	}
	
	void Awake()
	{
		agent = this.GetComponent<NavMeshAgent>();
	}
	void Start()
	{
		agent.speed = zombieMoveSpeed;
	}
	public void Update()
	{
		goToPlayer();
	}
	public void goToPlayer()
	{
		this.agent.SetDestination(GameManager.playerPosition);
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
