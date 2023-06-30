using UnityEngine;

namespace AI.Zombie
{

	//Abstract class for concrete states 
	// Zombie is paramater in fucntions  to pass data to parent (the zombie)
	public abstract class ZombieBaseState : MonoBehaviour
	{
		public abstract void EnterState(ZombieStateMachine manager);
		public abstract void FixedUpdateState(ZombieStateMachine manager);

		public abstract void UpdateState(ZombieStateMachine manager);
		public abstract void LateUpdateState(ZombieStateMachine manager);
		public abstract void TriggerEnter(ZombieStateMachine manager, Collider other);
		public abstract void TriggerStay(ZombieStateMachine manager, Collider other); //Called every Fixed Update
		public abstract void TriggerExit(ZombieStateMachine manager, Collider other);

		protected bool isPlayer(Collider other)
		{
			return other.GetComponent<PlayerRef>() != null;
		}
		protected bool isBreakable(Collider other)
		{
			return other.transform.GetComponent<IZombieBreakable>() != null;
		}
		protected void StopZombie(ZombieStateMachine manager)
		{
			if (manager.agent.isActiveAndEnabled)
				manager.agent.isStopped = true;
		}
		protected void UnstopZombie(ZombieStateMachine manager)
		{
			if (manager.agent.isActiveAndEnabled)
				manager.agent.isStopped = false;
		}
	}
}