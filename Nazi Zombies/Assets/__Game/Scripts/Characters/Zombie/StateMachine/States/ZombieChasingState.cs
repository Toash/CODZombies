using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	/// <summary>
	/// State for chasing the player
	/// </summary>
	public class ZombieChasingState : ZombieBaseState
	{
		[SerializeField]
		private float chaseSpeed = 2f;

		public override void EnterState(ZombieStateMachine manager)
		{
			manager.agent.speed = chaseSpeed;
			UnstopZombie(manager);
			//Debug.Log("Entering Chase state");
		}
		public override void UpdateState(ZombieStateMachine manager)
		{
			if (manager.agent.isActiveAndEnabled)
				manager.agent.SetDestination(PlayerRef.Instance.PlayerPosition());
			Debug.DrawLine(transform.position+Vector3.up, PlayerRef.Instance.PlayerPosition() + Vector3.up, Color.green);
		}

		public override void TriggerEnter(ZombieStateMachine manager, Collider other)
		{
			if (isPlayer(other))
			{
				manager.SwitchState(manager.AttackingState);
			}
			if (isBreakable(other))
			{
				manager.SwitchState(manager.BreakingState);
			}
		}

		public override void TriggerStay(ZombieStateMachine manager, Collider other)
		{

		}


		public override void TriggerExit(ZombieStateMachine manager, Collider other)
		{

		}

		public override void FixedUpdateState(ZombieStateMachine manager)
		{
			
		}

		public override void LateUpdateState(ZombieStateMachine manager)
		{
			
		}
	}
}