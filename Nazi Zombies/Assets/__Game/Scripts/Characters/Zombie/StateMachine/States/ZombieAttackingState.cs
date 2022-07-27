using UnityEngine;

namespace AI.Zombie
{

	//this is self contained?
	public class ZombieAttackingState : ZombieBaseState
	{
		IDamagable thingWeAreAttacking;

		private bool canAttack(ZombieStateManager zombie)
		{
			return zombie.stats.AttackSpeed <= timer;
		}

		private float timer;

		private void Update()
		{
			timer += Time.deltaTime;
		}

		public override void EnterState(ZombieStateManager manager)
		{
			//Debug.Log("Entering attack state");
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			if (thingWeAreAttacking == null)
			{
				manager.SwitchState(manager.ChasingState);
			}
			Debug.Log("In attack state");
		}

		public override void TriggerEnter(ZombieStateManager zombie, Collider other)
		{

		}
		public override void TriggerStay(ZombieStateManager manager, Collider other)
		{
			thingWeAreAttacking = other.GetComponent<IDamagable>();
			//damageable is favoriablre over player so this can also damage barricades
			if (thingWeAreAttacking!=null)
			{
				Attack(manager, other);
			}
		}

		public override void TriggerExit(ZombieStateManager manager, Collider other)
		{
		}
		private void Attack(ZombieStateManager manager, Collider other)
		{
			if (canAttack(manager))
			{
				Debug.Log("Attack");
				thingWeAreAttacking?.damage(manager.stats.Damage);
				timer = 0;
			}
		}


	}
}