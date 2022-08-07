using UnityEngine;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	/// <summary>
	/// Damaging player
	/// </summary>
	public class ZombieAttackingState : ZombieBaseState
	{
		[ShowInInspector, ReadOnly]
		private IDamagable attackent;

		[SerializeField]
		private float attackSpeed = 1.5f;
		[SerializeField]
		private int attackDamage = 50;
		private bool canAttack()
		{
			return attackSpeed <= timer;
		}

		private float timer;

		private void Update()
		{
			IncreaseTimer();
		}

		public override void EnterState(ZombieStateManager manager)
		{
			StopZombie(manager);
			attackent = PlayerRef.Instance.GetComponent<IDamagable>();
			/*
			Collider[] colliders = Physics.OverlapSphere(transform.position, 5, Physics.AllLayers, QueryTriggerInteraction.Ignore);

			foreach (Collider col in colliders)
			{
				if (isPlayer(col))
				{
					thingWeAreAttacking = col.GetComponent<IDamagable>();
				}
			}
			*/
		}
		public override void UpdateState(ZombieStateManager manager)
		{
			if (canAttack())
			{
				Attack();
			}
			if (attackent == null)
			{
				//manager.SwitchState(manager.ChasingState);
				Debug.LogError("No player or damageable found");
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
		private void Attack()
		{
			attackent.Damage(attackDamage);
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