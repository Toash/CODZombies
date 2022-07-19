using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class BaseInteractor : MonoBehaviour
{
	[SerializeField]
	private FloatVariable interactRadius;

	protected SphereCollider sphereCollider;
	protected virtual void Awake()
	{
		sphereCollider = GetComponent<SphereCollider>();
		sphereCollider.radius = interactRadius.Value;
		sphereCollider.isTrigger = true;
	}

	// For zombie, check if player or window
	//		Zombies shouldnt check for buyable
	// For player, check if buyable
	protected abstract void OnTriggerStay(Collider other);
}
