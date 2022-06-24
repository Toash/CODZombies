using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : MonoBehaviour,IDamagable,IKillable
{
	public int health { get; set; }
	[SerializeField] private int zombieMoveSpeed;
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
