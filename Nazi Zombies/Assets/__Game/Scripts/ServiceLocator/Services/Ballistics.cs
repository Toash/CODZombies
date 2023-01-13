using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class Ballistics : MonoBehaviour
{


    [SerializeField,InfoBox("What layers the bullet can collide with")]
    private LayerMask gunMask;

    [SerializeField]
    private GameObject bulletHole;
    [SerializeField]
    private int maxDecals = 15;

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
            //BEFORE ZOMBIE DEATH
            ApplyWeaponDamage(weapon, hit);

            //AFTER ZOMBIE DEATH
            ApplyKnockback(hit,weapon.Force); //Apply knockback AFTER DAMAGE, so that ragdoll is ready. 

            CreateBulletHole(hit);
            Debug.DrawRay(shooter.position, hit.point - shooter.position, Color.green, 10, false);
        }
    }

    private void ApplyWeaponDamage(Weapon weapon, RaycastHit hit)
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
