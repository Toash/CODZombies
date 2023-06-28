using UnityEngine;

namespace AI.Zombie
{

	//Abstract class for concrete states 
	// Zombie is paramater in fucntions  to pass data to parent (the zombie)
	public abstract class ZombieBaseState : MonoBehaviour
	{
		public abstract void EnterState(Zombie manager);
		public abstract void FixedUpdateState(Zombie manager);

		public abstract void UpdateState(Zombie manager);
		public abstract void LateUpdateState(Zombie manager);
		public abstract void TriggerEnter(Zombie manager, Collider other);
		public abstract void TriggerStay(Zombie manager, Collider other); //Called every Fixed Update
		public abstract void TriggerExit(Zombie manager, Collider other);

		protected bool isPlayer(Collider other)
		{
			return other.GetComponent<PlayerRef>() != null;
		}
		protected bool isBreakable(Collider other)
		{
			return other.transform.GetComponent<IZombieBreakable>() != null;
		}
		protected void StopZombie(Zombie manager)
		{
			manager.agent.isStopped = true;
		}
		protected void UnstopZombie(Zombie manager)
		{
			manager.agent.isStopped = false;
		}
	}
}