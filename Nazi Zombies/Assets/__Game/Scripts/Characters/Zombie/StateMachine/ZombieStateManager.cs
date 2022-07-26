using System;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class ZombieStateManager : MonoBehaviour
	{
		[ShowInInspector, ReadOnly]
		private ZombieBaseState currentState;

		[Title("Dependencies")]
		public NavMeshAgent Agent { get; private set; }


		//----------TRIGGER--------------
		public GameObject interactor;
		[ShowIf("interactor")]
		public Rigidbody rb;

		public ZombieStats stats;

		//-----------STATES--------------
		public ZombieChasingState ChasingState;
		public ZombieAttackingState AttackingState;

		private void Awake()
		{
			Agent = GetComponent<NavMeshAgent>();
		}

		private void Start()
		{
			SwitchState(ChasingState);
		}
		private void Update()
		{
			currentState.UpdateState(this);
		}

		//--------------PUBLIC METHODS---------------

		public void SwitchState(ZombieBaseState state)
		{
			currentState = state;
			currentState.EnterState(this);
		}

		//--------------PHYSICS---------------

		private void OnTriggerEnter(Collider other)
		{
			currentState.TriggerEnter(this, other);
		}
		private void OnTriggerStay(Collider other)
		{
			currentState.TriggerStay(this, other);
		}
		private void OnTriggerExit(Collider other)
		{
			currentState.TriggerExit(this, other);
		}


	}
}