using UnityEngine;

public static class Ballistics
{
	public static void CreateBullet(int damage,Vector3 origin, Vector3 dir, float range)
	{
		RaycastHit hit;
		if (Physics.Raycast(origin, dir, out hit, range))
		{
			//hit something
			//TODO: create bullet hole
			IDamagable damageable = hit.transform.GetComponent<IDamagable>();
			if(damageable != null)
			{
				//hit something that has functionality when hit
				damageable.damage(damage);
			}
		}
		//didnt hit
	}
	public static void CreateExplosion(int damage, Vector3 origin, float range)
	{

	}
}
