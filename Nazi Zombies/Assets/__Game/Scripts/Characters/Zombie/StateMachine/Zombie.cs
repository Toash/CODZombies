using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	/// <summary>
	/// Base zombie class
	/// </summary>
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(Rigidbody))]
	public class Zombie : MonoBehaviour
	{
		[HideInInspector]
        public NavMeshAgent agent;

        //----------STARTING STATS---------
        public int StartingHealth = 100;
        public float StartingSpeed = 2f; // nav agent speed

		// Depracted: Past a certain speed the zombie will automatically run. 
        //public bool StartsRunning = false; //later rounds the zombie will run.

        //-----------STATES--------------
        [ShowInInspector, ReadOnly]
		private ZombieBaseState currentState;

        public ZombieChasingState ChasingState;
		public ZombieAttackingState AttackingState;
		public ZombieBreakingState BreakingState;
		public ZombieDeadState DeadState;

		[Title("Events")]
		public UnityEvent Chase;
		public UnityEvent Attacking;
		public UnityEvent Breaking;
		public UnityEvent Dead;

		private void Awake()
        {
			agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
		{
			SwitchState(ChasingState); //When zombie spawns, chase the player
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
		public void Kill()
        {
			currentState = DeadState;
			currentState.EnterState(this);
			Dead?.Invoke();
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