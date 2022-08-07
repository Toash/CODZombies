using System;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class ZombieStateManager : MonoBehaviour
	{
		[ShowInInspector, ReadOnly]
		private ZombieBaseState currentState;

		[ShowInInspector, ReadOnly]
		public NavMeshAgent Agent { get; private set; }
		[ShowInInspector, ReadOnly]
		public Collider Col { get; private set; }

		//-----------STATES--------------
		public ZombieChasingState ChasingState;
		public ZombieAttackingState AttackingState;
		public ZombieBreakingState BreakingState;
		public ZombieDeadState DeadState;

        private void Awake()
        {
			Agent = GetComponent<NavMeshAgent>();
			Col = GetComponent<Collider>();
        }
        private void Start()
		{
			SwitchState(ChasingState);
		}
		private void Update()
		{
			currentState.UpdateState(this);
		}
		private void FixedUpdate()
		{
			currentState.FixedUpdateState(this);
		}
		private void LateUpdate()
		{
			currentState.LateUpdateState(this);
		}

		//--------------PUBLIC METHODS---------------

		public void SwitchState(ZombieBaseState state)
		{
			currentState = state;
			currentState.EnterState(this);
		}
		public void Dead()
        {
			currentState = DeadState;
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