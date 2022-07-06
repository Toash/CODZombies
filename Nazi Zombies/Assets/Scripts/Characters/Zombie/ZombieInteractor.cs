using UnityEngine;
using System.Collections;

namespace Zombie
{
	//handles zombie attacking player, and breaking barricades. 
	public class ZombieInteractor : Interactor
	{
		[Header("Zombie Stats")]
		[SerializeField]
		private IntVariable zombieDamage;
		[SerializeField]
		private FloatVariable zombieAttackSpeed;
		[SerializeField]
		private FloatVariable zombieBarricadeBreakSpeed;

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
			//if the collider isn't a zombie and has IDamagable
			if (other != this&&other.GetComponent<IDamagable>()!=null&&canAttack)
			{
				IDamagable damagable = other.GetComponent<IDamagable>();
				ZombieDamage(damagable);
			}
		}

		private void ZombieDamage(IDamagable damagable)
		{
			Debug.Log("Zombie attacking");
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

