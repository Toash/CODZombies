using System;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	[RequireComponent(typeof(Rigidbody))]
	public class ZombieStateManager : MonoBehaviour
	{
		[ShowInInspector, ReadOnly]
		private ZombieBaseState currentState;

		[Title("Dependencies")]
		[field: SerializeField]
		public NavMeshAgent Agent { get; private set; }

		//-----------STATES--------------
		public ZombieChasingState ChasingState;
		public ZombieAttackingState AttackingState;
		public ZombieBreakingState BreakingState;


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