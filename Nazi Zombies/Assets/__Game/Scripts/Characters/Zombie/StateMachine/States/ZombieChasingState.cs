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

		public override void EnterState(Zombie manager)
		{
			manager.agent.speed = chaseSpeed;
			UnstopZombie(manager);
			//Debug.Log("Entering Chase state");
		}
		public override void UpdateState(Zombie manager)
		{
			manager.agent.SetDestination(PlayerRef.Instance.PlayerPosition());
			Debug.DrawLine(transform.position+Vector3.up, PlayerRef.Instance.PlayerPosition() + Vector3.up, Color.green);
		}

		public override void TriggerEnter(Zombie manager, Collider other)
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

		public override void TriggerStay(Zombie manager, Collider other)
		{

		}


		public override void TriggerExit(Zombie manager, Collider other)
		{

		}

		public override void FixedUpdateState(Zombie manager)
		{
			
		}

		public override void LateUpdateState(Zombie manager)
		{
			
		}
	}
}