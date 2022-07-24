using UnityEngine;
using Sirenix.OdinInspector;

public class ZombieChasingState : ZombieBaseState,IZombieUpdatable,IZombieTriggerEnter,IZombieTriggerStay
{
	private bool isPlayer(Collision other)
	{
		return other.gameObject.GetComponent<PlayerRef>()!=null;
	}
	private bool isDamageable(Collision other)
	{
		return other.gameObject.GetComponent<IDamagable>()!=null;
	}

	public void UpdateState(ZombieStateManager zombie)
	{
		zombie.agent.SetDestination(PlayerRef.Instance.PlayerPosition());
	}

	public void OnTriggerEnter(ZombieStateManager zombie, Collision other)
	{
		if (isPlayer(other)&&isDamageable(other))
		{
			zombie.SwitchState(zombie.AttackingState);
		}
	}
	public void OnTriggerStay(ZombieStateManager zombie, Collision other)
	{
		//if Breakable, break
		//if Damageable, damage
		
	}


}
