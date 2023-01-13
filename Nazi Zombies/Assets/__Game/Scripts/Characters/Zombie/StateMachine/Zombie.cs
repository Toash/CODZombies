using System;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	/// <summary>
	/// Base zombie class
	/// </summary>
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	public class Zombie : MonoBehaviour
	{
        public NavMeshAgent agent;
        public Collider col;

        //----------STARTING STATS---------
        public int StartingHealth = 100;
        public float StartingSpeed = 2f; // nav agent speed
        public bool StartsRunning = false;

        //-----------STATES--------------
        [ShowInInspector, ReadOnly]
		private ZombieBaseState currentState;

        public ZombieChasingState ChasingState;
		public ZombieAttackingState AttackingState;
		public ZombieBreakingState BreakingState;
		public ZombieDeadState DeadState;



        private void Start()
		{
			SwitchState(ChasingState);
            ServLoc.Inst.GameManager.AddZombieCount();
        }
        private void OnDisable() {
            ServLoc.Inst.GameManager.RemoveZombieCount();
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