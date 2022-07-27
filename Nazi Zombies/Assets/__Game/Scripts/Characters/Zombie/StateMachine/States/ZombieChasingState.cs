using UnityEngine;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	public class ZombieChasingState : ZombieBaseState
	{
		public override void EnterState(ZombieStateManager manager)
		{
			UnstopZombie(manager);
			//Debug.Log("Entering Chase state");
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			manager.Agent.SetDestination(PlayerRef.Instance.PlayerPosition());
			Debug.Log("In Chase State");
		}

		public override void TriggerEnter(ZombieStateManager manager, Collider other)
		{
			//other cannot be this gameobject
			if (isDamageable(other))
			{
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