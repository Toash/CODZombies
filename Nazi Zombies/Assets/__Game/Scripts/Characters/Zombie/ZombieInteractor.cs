using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;


namespace AI.Zombie
{
	//handles zombie attacking player, and breaking barricades. 
	public class ZombieInteractor : MonoBehaviour
	{
		[SerializeField]
		private ZombieStats stats;
		[SerializeField]
		private SphereCollider interactCollider;

		[Header("Unity Events")]
		[SerializeField]
		private UnityEvent zombieAttackedEvent;

		private float timer;

		private bool canAttack { get { return stats.AttackSpeed <= timer; } }

		private void Update()
		{
			UpdateTimer();
		}

		private void OnTriggerStay(Collider other)
		{
			IDamagable damagable = other.GetComponent<IDamagable>();
			if (other!=this&&damagable!=null&&canAttack)
			{
				Attack(damagable);
			}
		}

		private void Attack(IDamagable damagable)
		{
			damagable.damage(stats.Damage);
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

