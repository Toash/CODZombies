using UnityEngine;
using System.Collections.Generic;

public class Ballistics : MonoBehaviour
{


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
        if (HitSomething(shooter, weapon, out hit))
        {
			Collider[] colliders = Physics.OverlapSphere(hit.point, EXPLOSION_RANGE, Physics.AllLayers, QueryTriggerInteraction.Ignore);
			foreach(Collider col in colliders)
            {
				Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
					rb.AddExplosionForce(100f, hit.point, EXPLOSION_RANGE, 0, ForceMode.Impulse);
                }
            }
            hit.transform.GetComponent<IDamagable>()?.Damage(weapon.Damage);


            Debug.DrawRay(shooter.position, hit.point - shooter.position, Color.green, 10, false);
            bulletHole(hit, weapon.BulletHole);
        }
    }

    private void bulletHole(RaycastHit hitPoint, GameObject bulletHole)
	{
		GameObject bullet = Instantiate(bulletHole, hitPoint.point+(hitPoint.normal*.01f), Quaternion.LookRotation(-hitPoint.normal, Vector3.up),hitPoint.transform);
		decals.Enqueue(bullet);
	}
	private bool HitSomething(Transform shooter, Weapon weapon, out RaycastHit hit)
	{
		return Physics.Raycast(shooter.position, shooter.forward, out hit, weapon.Range, weapon.WhatToHit, QueryTriggerInteraction.Ignore);
	}
}
