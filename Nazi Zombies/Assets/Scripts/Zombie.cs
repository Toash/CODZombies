using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Zombie : Entity
{
	private NavMeshAgent agent;

	void Awake()
	{
		agent = this.GetComponent<NavMeshAgent>();
	}
	void Start()
	{
		agent.speed = moveSpeed;
	}

	protected override void die()
	{
		Debug.Log("Zombie dead");
	}


	public override void Update()
	{
		goToPlayer();
	}

	private void goToPlayer()
	{
		this.agent.SetDestination(GameManager.playerPosition);
	}



}
