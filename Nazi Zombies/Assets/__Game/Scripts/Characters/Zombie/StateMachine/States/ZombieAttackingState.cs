using UnityEngine;
using Sirenix.OdinInspector;

namespace AI.Zombie
{

	//this is self contained?
	//not really :/
	/// <summary>
	/// Damaging player
	/// </summary>
	public class ZombieAttackingState : ZombieBaseState
	{
		[ShowInInspector,ReadOnly]
		private IDamagable thingWeAreAttacking;

		private bool canAttack(ZombieStateManager manager)
		{
			return manager.stats.AttackSpeed <= timer;
		}

		private float timer;

		private void Update()
		{
			IncreaseTimer();
		}

		public override void EnterState(ZombieStateManager manager)
		{
			StopZombie(manager);
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			//if it has IZombieBreakable?
			if (thingWeAreAttacking != null)
			{
				
				Attack(manager);
			}
			if (thingWeAreAttacking == null)
			{
				manager.SwitchState(manager.ChasingState);
			}
		}
		public override void FixedUpdateState(ZombieStateManager manager)
		{

		}

		public override void LateUpdateState(ZombieStateManager manager)
		{

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
				thingWeAreAttacking.Damage(manager.stats.Damage);
				ResetTimer();
			}
		}

		private void IncreaseTimer()
		{
			timer += Time.deltaTime;
		}
		private void ResetTimer()
		{
			timer = 0;
		}


	}
}