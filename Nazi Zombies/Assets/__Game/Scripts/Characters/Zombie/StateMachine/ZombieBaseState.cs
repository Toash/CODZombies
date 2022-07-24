using UnityEngine;

//Abstract class for concrete states 
//The concrete states are SELF CONTRAINED.
public abstract class ZombieBaseState:MonoBehaviour
{
	//We put ZombieStateManager for the context when a concrete state implments these methods
	/*public abstract void EnterState(ZombieStateManager zombie);
	public abstract void UpdateState(ZombieStateManager zombie);
	public abstract void OnTriggerEnter(ZombieStateManager zombie,Collision other);
	public abstract void OnTriggerStay(ZombieStateManager zombie, Collision other);*/

}
