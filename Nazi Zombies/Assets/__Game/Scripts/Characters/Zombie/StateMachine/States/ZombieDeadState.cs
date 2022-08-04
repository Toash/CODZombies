using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AI.Zombie
{
	public class ZombieDeadState : ZombieBaseState
	{
		[SerializeField]
		private UnityEvent deathEvent;
		public override void EnterState(ZombieStateManager manager)
		{
			manager.Agent.enabled = false;
			deathEvent?.Invoke();

			//stop navmeshagent
			//disable health component


			//ragdoll
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			
		}
		public override void TriggerEnter(ZombieStateManager zombie, Collider other)
		{
			
		}
		public override void TriggerStay(ZombieStateManager zombie, Collider other)
		{
			
		}
		public override void TriggerExit(ZombieStateManager zombie, Collider other)
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