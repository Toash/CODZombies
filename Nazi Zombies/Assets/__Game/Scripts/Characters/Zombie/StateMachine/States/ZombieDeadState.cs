using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	[InfoBox("By default, disables NavMeshAgent and Root Collider")]
	public class ZombieDeadState : ZombieBaseState
	{
		[SerializeField]
		private UnityEvent deathEvent;

		public override void EnterState(ZombieStateManager manager)
		{
			manager.Agent.enabled = false;
			manager.Col.enabled = false;
			deathEvent?.Invoke();
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