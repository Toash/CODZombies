using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	public class ZombieChasingState : ZombieBaseState
	{
		[SerializeField]
		private float chaseSpeed = 2f;

		public override void EnterState(ZombieStateManager manager)
		{
			manager.Agent.speed = chaseSpeed;
			UnstopZombie(manager);
			//Debug.Log("Entering Chase state");
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			manager.Agent.SetDestination(PlayerRef.Instance.PlayerPosition());
			Debug.DrawLine(transform.position+Vector3.up, PlayerRef.Instance.PlayerPosition() + Vector3.up, Color.green);
		}

		public override void TriggerEnter(ZombieStateManager manager, Collider other)
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

		public override void TriggerStay(ZombieStateManager manager, Collider other)
		{

		}


		public override void TriggerExit(ZombieStateManager manager, Collider other)
		{

		}

		public override void FixedUpdateState(ZombieStateManager manager)
		{
			
		}

		public override void LateUpdateState(ZombieStateManager manager)
		{
			
		}
	}
}