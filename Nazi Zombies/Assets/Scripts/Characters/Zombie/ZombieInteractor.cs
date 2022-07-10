using UnityEngine;
using UnityEngine.Events;


namespace AI.Zombie
{
	//handles zombie attacking player, and breaking barricades. 
	public class ZombieInteractor : BaseInteractor
	{
		[Header("Zombie Stats")]
		[SerializeField]
		private IntVariable zombieDamage;
		[SerializeField]
		private FloatVariable zombieAttackSpeed;
		[SerializeField]
		private FloatVariable zombieBarricadeBreakSpeed;
		[Header("Unity Events")]
		[SerializeField]
		private UnityEvent zombieAttackedEvent;

		private float timer;

		private bool canAttack { get { return zombieAttackSpeed.Value <= timer; } }

		private void Update()
		{
			UpdateTimer();
		}

		protected override void OnTriggerStay(Collider other)
		{
			IDamagable damagable = other.GetComponent<IDamagable>();
			if (other!=this&&damagable!=null&&canAttack)
			{
				Attack(damagable);
			}
		}

		private void Attack(IDamagable damagable)
		{
			damagable.damage(zombieDamage.Value);
			zombieAttackedEvent?.Invoke();
			ResetTimer();
		}

		private void UpdateTimer()
		{
			timer += Time.deltaTime;
		}

		private void ResetTimer()
		{
			timer = 0;
		}


	}
}

