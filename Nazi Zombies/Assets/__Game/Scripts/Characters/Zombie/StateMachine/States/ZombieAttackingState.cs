using UnityEngine;

namespace AI.Zombie
{

	//this is self contained?
	public class ZombieAttackingState : ZombieBaseState
	{
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
			
		}

		public override void TriggerEnter(ZombieStateManager zombie, Collider other)
		{

		}
		public override void TriggerStay(ZombieStateManager manager, Collider other)
		{
			//damageable is favoriablre over player so this can also damage barricades
			if (isDamageable(other))
			{
				Attack(manager,other);
			}
		}

		public override void TriggerExit(ZombieStateManager manager, Collider other)
		{
			if (isPlayer(other))
			{
				manager.SwitchState(manager.ChasingState);
			}
		}
		private void Attack(ZombieStateManager manager,Collider other)
		{
			if (canAttack(manager))
			{
				Debug.Log("Attack");
				var damagable = other.transform.GetComponent<IDamagable>();
				damagable?.damage(manager.stats.Damage);
				timer = 0;
			}
		}


	}
}