using UnityEngine;

namespace AI.Zombie
{

	//Abstract class for concrete states 
	//The concrete states are SELF CONTRAINED.
	public abstract class ZombieBaseState : MonoBehaviour
	{
		public abstract void EnterState(ZombieStateManager manager);
		public abstract void FixedUpdateState(ZombieStateManager manager);

		public abstract void UpdateState(ZombieStateManager manager);
		public abstract void LateUpdateState(ZombieStateManager manager);
		public abstract void TriggerEnter(ZombieStateManager manager, Collider other);
		public abstract void TriggerStay(ZombieStateManager manager, Collider other); //Called every Fixed Update
		public abstract void TriggerExit(ZombieStateManager manager, Collider other);

		protected bool isPlayer(Collider other)
		{
			return other.transform.GetComponent<IDamagable>() != null && other.GetComponent<PlayerRef>() != null;
		}
		protected bool isBarricade(Collider other)
		{
			return other.transform.GetComponent<Barricade>() != null;
		}


		protected void StopZombie(ZombieStateManager manager)
		{
			manager.Agent.isStopped = true;
		}
		protected void UnstopZombie(ZombieStateManager manager)
		{
			manager.Agent.isStopped = false;
		}
	}
}