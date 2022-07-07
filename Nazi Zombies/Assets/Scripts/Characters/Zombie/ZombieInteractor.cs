using UnityEngine;
using UnityEngine.Events;


namespace AI.Zombie
{
	[System.Serializable]
	public class DamageEvent : UnityEvent<MonoBehaviour>
	{
	}
	//handles zombie attacking player, and breaking barricades. 
	//	
	public class ZombieInteractor : Interactor
	{
		[Header("Zombie Stats")]
		[SerializeField]
		private IntVariable zombieDamage;
		[SerializeField]
		private FloatVariable zombieAttackSpeed;
		[SerializeField]
		private FloatVariable zombieBarricadeBreakSpeed;
		[Header("UnityEvents")]

		private float timer;

		private bool canAttack
		{
			get
			{
				return zombieAttackSpeed.Value <= timer;
			}
		}

		private void Update()
		{
			UpdateTimer();
		}
		protected override void OnTriggerEnter(Collider other)
		{

		}

		protected override void OnTriggerStay(Collider other)
		{
			IDamagable damagable = other.GetComponent<IDamagable>();
			if (other!=this&&damagable!=null&&canAttack)
			{
				ZombieDamage(damagable);
			}
		}

		private void ZombieDamage(IDamagable damagable)
		{
			damagable.damage(zombieDamage.Value);
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

