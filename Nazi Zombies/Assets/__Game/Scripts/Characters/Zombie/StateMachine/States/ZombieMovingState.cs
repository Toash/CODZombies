using UnityEngine;

public class ZombieMovingState : ZombieBaseState
{
	public override void EnterState(ZombieStateManager zombie)
	{

	}
	public override void UpdateState(ZombieStateManager zombie)
	{
		zombie.agent.SetDestination(PlayerRef.Instance.PlayerPosition());
	}

	public override void OnTriggerEnter(ZombieStateManager zombie, Collision other)
	{
		if (other.gameObject.GetComponent<IDamagable>() != null)
		{
			zombie.SwitchState(zombie.AttackingState);
		}
	}
	public override void OnTriggerStay(ZombieStateManager zombie, Collision other)
	{
		//if Breakable, break
		//if Damageable, damage
		
	}


}
