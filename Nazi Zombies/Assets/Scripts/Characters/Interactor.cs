using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Interactor : MonoBehaviour
{
	protected SphereCollider sphereCollider;
	protected void Awake()
	{
		sphereCollider = GetComponent<SphereCollider>();	
	}

	// For zombie, check if player or window
	//		Zombies shouldnt check for buyable
	// For player, check if buyable
	protected abstract void OnTriggerEnter(Collider other);
	protected abstract void OnTriggerStay(Collider other);
}
