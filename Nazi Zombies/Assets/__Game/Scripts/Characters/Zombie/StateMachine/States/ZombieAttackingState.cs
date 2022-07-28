using UnityEngine;

namespace AI.Zombie
{

	//this is self contained?
	//not really :/
	public class ZombieAttackingState : ZombieBaseState
	{
		private IDamagable thingWeAreAttacking;

		private bool canAttack(ZombieStateManager manager)
		{
			return manager.stats.AttackSpeed <= timer;
		}

		private float timer;

		private void Update()
		{
			timer += Time.deltaTime;
		}

		public override void EnterState(ZombieStateManager manager)
		{
			StopZombie(manager);
			//Debug.Log("Entering attack state");
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			if (thingWeAreAttacking != null)
			{
				Attack(manager);
			}
			else
			{
				//Thing we are attacking is no longer there :O
				manager.SwitchState(manager.ChasingState);
			}
			//Debug.Log("In attack state");
		}

		public override void TriggerEnter(ZombieStateManager zombie, Collider other)
		{

		}
		public override void TriggerStay(ZombieStateManager manager, Collider other)
		{
			thingWeAreAttacking = other.GetComponent<IDamagable>();
		}

		public override void TriggerExit(ZombieStateManager manager, Collider other)
		{
		}
		private void Attack(ZombieStateManager manager)
		{
			if (canAttack(manager))
			{
				//Debug.Log("Attack");
				thingWeAreAttacking?.Damage(manager.stats.Damage);
				timer = 0;
			}
		}


	}
}