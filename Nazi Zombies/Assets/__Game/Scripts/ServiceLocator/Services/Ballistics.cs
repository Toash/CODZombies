using UnityEngine;
using System.Collections.Generic;

public class Ballistics:MonoBehaviour
{
	private Queue<GameObject> decals = new Queue<GameObject>();

	private readonly int MAX_DECALS = 5;

	private void Update()
	{
		if (decals.Count >= MAX_DECALS)
		{
			GameObject bullet = decals.Dequeue();
			Destroy(bullet);
		}
	}

	public void CastBullet(Transform shooter, Weapon weapon)
	{
		RaycastHit hit;
		if (Physics.Raycast(shooter.position, shooter.forward, out hit, weapon.Range, weapon.WhatToHit, weapon.ShouldHitTriggers))
		{
			IDamagable damageable = hit.transform.GetComponent<IDamagable>();
			if (damageable != null)
			{
				damageable.damage(weapon.Damage);
			}
			Debug.DrawRay(shooter.position, hit.point-shooter.position,Color.green,10,false);
			bulletHole(hit, weapon.BulletHole);
		}
		//didnt hit
	}
	public void CreateExplosion(int damage, Vector3 origin, float range)
	{

	}
	private void bulletHole(RaycastHit hitPoint, GameObject bulletHole)
	{
		//GameObject bullet = Instantiate
		GameObject bullet = Instantiate(bulletHole, hitPoint.point+(hitPoint.normal*.01f), Quaternion.LookRotation(-hitPoint.normal, Vector3.up),hitPoint.transform);
		decals.Enqueue(bullet);
	}
}
