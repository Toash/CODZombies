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
	public class ZombieStateMachine : MonoBehaviour
	{
		[HideInInspector]
        public NavMeshAgent agent;

        //----------STARTING STATS---------
        public float StartingSpeed = 2f; // nav agent speed

		// Depracted: Past a certain speed the zombie will automatically run. 
        //public bool StartsRunning = false; //later rounds the zombie will run.

        //-----------STATES--------------
        [ShowInInspector, ReadOnly]
		private ZombieBaseState currentState;

		[Tooltip("Gameobject that contains states")]
		public GameObject statesObject;


		[HideInInspector] 
		public ZombieChasingState ChasingState;
		[HideInInspector] 
		public ZombieAttackingState AttackingState;
		[HideInInspector] 
		public ZombieBreakingState BreakingState;
		

		[Title("Events")]
		public UnityEvent Chase;
		public UnityEvent Attacking;
		public UnityEvent Breaking;

		private void Awake()
        {
			ChasingState = statesObject.GetComponent<ZombieChasingState>();
			AttackingState = statesObject.GetComponent<ZombieAttackingState>();
			BreakingState = statesObject.GetComponent<ZombieBreakingState>();
			agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
		{
			SwitchState(ChasingState); //When zombie spawns, chase the player
            ServLoc.Inst.GameManager.AddZombieCount();
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
			ServLoc.Inst.GameManager.RemoveZombieCount();
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