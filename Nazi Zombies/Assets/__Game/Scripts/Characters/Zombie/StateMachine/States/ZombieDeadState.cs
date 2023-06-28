using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	/// <summary>
	/// State for when zombie dies
	/// </summary>
	[InfoBox("By default, disables NavMeshAgent and Root Collider")]
	public class ZombieDeadState : ZombieBaseState
	{

		public override void EnterState(Zombie manager)
		{
			manager.agent.enabled = false;
		}
		public override void UpdateState(Zombie manager)
		{
			
		}
		public override void TriggerEnter(Zombie zombie, Collider other)
		{
			
		}
		public override void TriggerStay(Zombie zombie, Collider other)
		{
			
		}
		public override void TriggerExit(Zombie zombie, Collider other)
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