using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AI.Zombie
{
	public class ZombieDeadState : ZombieBaseState
	{

		public override void EnterState(ZombieStateManager manager)
		{
			Debug.Log("Zombie is dead");
			
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