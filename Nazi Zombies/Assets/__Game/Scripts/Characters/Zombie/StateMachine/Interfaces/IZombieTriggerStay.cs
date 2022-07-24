using UnityEngine;

public interface IZombieTriggerStay
{
	public void OnTriggerStay(ZombieStateManager zombie, Collision other);
}
