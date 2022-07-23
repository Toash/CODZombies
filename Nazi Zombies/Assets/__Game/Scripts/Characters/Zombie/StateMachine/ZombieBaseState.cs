using UnityEngine;

//Abstract class for concrete states 
public abstract class ZombieBaseState
{
	//We put ZombieStateManager for the context when a concrete state implments these methods
	public abstract void EnterState(ZombieStateManager zombie);
	public abstract void UpdateState(ZombieStateManager zombie);
	public abstract void OnTriggerEnter(ZombieStateManager zombie,Collision other);
	public abstract void OnTriggerStay(ZombieStateManager zombie, Collision other);

}
