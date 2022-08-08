using UnityEngine;
using System.Collections.Generic;

public class Ballistics : MonoBehaviour
{


	[SerializeField]
	private int maxDecals = 15;
    [SerializeField]
    private LayerMask gunMask;
    [SerializeField]
    private GameObject bulletHole;

	private Queue<GameObject> decals = new Queue<GameObject>();

	private readonly float EXPLOSION_RANGE = .1f;

	private void Update()
	{
		if (decals.Count >= maxDecals)
		{
			GameObject bullet = decals.Dequeue();
			Destroy(bullet);
		}
	}

	public void CastBullet(Transform shooter, Weapon weapon)
    {
        RaycastHit hit;
        if (Physics.Raycast(shooter.position, shooter.forward, out hit, weapon.Range, this.gunMask, QueryTriggerInteraction.Ignore))
        {
            ApplyDamage(weapon, hit);
            CreateBulletHole(hit);

            //Call knockback AFTER DAMAGE, so that ragdoll is ready. 
            ApplyKnockback(hit,weapon.Force);

            Debug.DrawRay(shooter.position, hit.point - shooter.position, Color.green, 10, false);

        }
    }

    private void ApplyDamage(Weapon weapon, RaycastHit hit)
    {
        hit.transform.GetComponent<IDamagable>()?.Damage(weapon.Damage);
    }

    private void ApplyKnockback(RaycastHit hit, float force)
    {
        Collider[] colliders = Physics.OverlapSphere(hit.point, EXPLOSION_RANGE, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        foreach (Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, hit.point, EXPLOSION_RANGE, 0, ForceMode.Impulse);
            }
        }
    }

    private void CreateBulletHole(RaycastHit hitPoint)
	{
		GameObject bullet = Instantiate(bulletHole, hitPoint.point+(hitPoint.normal*.01f), Quaternion.LookRotation(-hitPoint.normal, Vector3.up),hitPoint.transform);
		decals.Enqueue(bullet);
	}
}
