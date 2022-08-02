using UnityEngine;

namespace AI.Zombie
{

	//Controls the zombie falling over
	public class ZombieRagdoll : MonoBehaviour
	{
		//public Collider col;
		public Rigidbody rb;
		public Collider ragdollCollider;

		public void Ragdoll()
		{
			rb.isKinematic = false;
			rb.useGravity = true;
			ragdollCollider.gameObject.layer = LayerMask.NameToLayer("GroundOnly");
		}
	}
}