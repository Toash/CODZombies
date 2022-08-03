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
		[ShowInInspector, ReadOnly]
		private IDamagable thingWeAreAttacking;

		[SerializeField]
		private float attackSpeed;
		[SerializeField]
		private float attackDamage;
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
			Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

			foreach (Collider col in colliders)
			{
				if (isPlayer(col))
				{
					thingWeAreAttacking = col.GetComponent<IDamagable>();
				}
			}
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			if (canAttack(manager))
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

		}

		public override void TriggerExit(ZombieStateManager manager, Collider other)
		{
			if (isPlayer(other))
			{
				manager.SwitchState(manager.ChasingState);
			}
		}
		private void Attack(ZombieStateManager manager)
		{
			thingWeAreAttacking.Damage(manager.stats.Damage);
			ResetTimer();
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