using UnityEngine;

namespace AI.Zombie
{

	public class ZombieRagdoll : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody mainRb;
		[SerializeField]
		private Collider mainCollider;
		[SerializeField]
		private Transform ragdollRoot;

		public void Knockback(float force)
        {

        }

		public void Ragdoll()
		{
			SetRagdollIsKinematicAndGravity(ragdollRoot, mainRb, false);
			SetRagdollColliderEnabled(ragdollRoot, mainCollider, true);
		}

		private void SetRagdollIsKinematicAndGravity(Transform ragdollRoot, Rigidbody mainRb, bool state)
        {
			mainRb.isKinematic = !state;
			mainRb.useGravity = state;
			
			foreach(Rigidbody rb in ragdollRoot)
            {
				rb.isKinematic = state;
				rb.useGravity = !state;
            }
        }
		private void SetRagdollColliderEnabled(Transform ragdollRoot, Collider mainCol, bool state)
        {
			mainCol.enabled = !state;
			foreach(Collider col in ragdollRoot)
            {
				col.enabled = state; 
            }
        }
    }
}