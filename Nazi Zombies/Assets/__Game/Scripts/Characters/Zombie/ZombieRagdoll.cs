using UnityEngine;

namespace AI.Zombie
{

	public class ZombieRagdoll : MonoBehaviour
	{
		[SerializeField]
		private Transform ragdollRoot;

		public void Ragdoll()
		{
			SetRagdollIsKinematicAndGravity(ragdollRoot,false);
			SetRagdollColliderEnabled(ragdollRoot,true);
		}

		private void SetRagdollIsKinematicAndGravity(Transform ragdollRoot, bool state)
        {

			Rigidbody[] rbs = ragdollRoot.GetComponentsInChildren<Rigidbody>();
			foreach(Rigidbody rb in rbs)
            {
				rb.isKinematic = state;
				rb.useGravity = !state;
            }
        }
		private void SetRagdollColliderEnabled(Transform ragdollRoot,bool state)
        {
			Collider[] cols = ragdollRoot.GetComponentsInChildren<Collider>();
			foreach (Collider col in cols)
            {
				col.enabled = state; 
            }
        }
    }
}