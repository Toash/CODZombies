using UnityEngine;

namespace Zombie
{
	public class ZombieInteractor : Interactor
	{
		[Header("Zombie Stats")]
		[SerializeField]
		private IntVariable zombieDamage;
		[SerializeField]
		private FloatVariable zombieAttackSpeed;
		[SerializeField]
		private FloatVariable zombieWindowBreakSpeed;

		protected override void OnTriggerEnter(Collider other)
		{
			//if the collider isn't a zombie and has IDamagable
			if (other != this&&other.GetComponent<IDamagable>()!=null)
			{
				IDamagable damagable = other.GetComponent<IDamagable>();
				damagable.damage(zombieDamage.Value);
			}
		}
	}
}

