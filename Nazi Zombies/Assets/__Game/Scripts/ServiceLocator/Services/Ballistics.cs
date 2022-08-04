using UnityEngine;
using System.Collections.Generic;

public class Ballistics : MonoBehaviour
{


	[SerializeField]
	private int maxDecals = 15;

	private Queue<GameObject> decals = new Queue<GameObject>();

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
		if (Physics.Raycast(shooter.position, shooter.forward, out hit, weapon.Range, weapon.WhatToHit, QueryTriggerInteraction.Ignore))
		{
			hit.transform.GetComponent<IDamagable>()?.Damage(weapon.Damage);
			hit.transform.GetComponent<IKnockbackable>()?.Knockback(1000f);

			Debug.DrawRay(shooter.position, hit.point-shooter.position,Color.green,10,false);
			bulletHole(hit, weapon.BulletHole);
		}
		//MISS
	}
	private void bulletHole(RaycastHit hitPoint, GameObject bulletHole)
	{
		GameObject bullet = Instantiate(bulletHole, hitPoint.point+(hitPoint.normal*.01f), Quaternion.LookRotation(-hitPoint.normal, Vector3.up),hitPoint.transform);
		decals.Enqueue(bullet);
	}
}
