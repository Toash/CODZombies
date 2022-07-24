using UnityEngine;

//this is self contained?
public class ZombieAttackingState : ZombieBaseState,IZombieTriggerStay
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

	private bool isPlayer(Collision other)
	{
		return other.gameObject.GetComponent<PlayerRef>() != null;
	}
	private bool isDamageable(Collision other)
	{
		return other.gameObject.GetComponent<IDamagable>() != null;
	}

	public void OnTriggerStay(ZombieStateManager zombie, Collision other)
	{
		//attack player
		
		if (isPlayer(other)&&isDamageable(other))
		{
			
		}
	}

	private void Attack(ZombieStateManager zombie, Collision other)
	{
		var damagable = other.transform.GetComponent<IDamagable>();

		timer = 0;
	}


}
