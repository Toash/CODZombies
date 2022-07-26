using UnityEngine;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	public class ZombieChasingState : ZombieBaseState
	{

		public override void EnterState(ZombieStateManager manager)
		{
			UnstopZombie(manager);
			Debug.Log("Entering Chase state");
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			manager.Agent.SetDestination(PlayerRef.Instance.PlayerPosition());
			Debug.Log("Chasing");
		}

		public override void TriggerEnter(ZombieStateManager manager, Collider other)
		{
			if (isPlayer(other))
			{
				StopZombie(manager);
				manager.SwitchState(manager.AttackingState);
			}
		}

		public override void TriggerStay(ZombieStateManager manager, Collider other)
		{

		}


		public override void TriggerExit(ZombieStateManager manager, Collider other)
		{

		}

	}
}