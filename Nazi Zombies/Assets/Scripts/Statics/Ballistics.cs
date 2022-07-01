using UnityEngine;

public static class Ballistics
{
	public static void Shoot(int damage,Vector3 origin, Vector3 dir, float range)
	{
		Debug.Log("shooting");
		RaycastHit hit;
		if (Physics.Raycast(origin, dir, out hit, range))
		{
			//hit something
			IDamagable damageable = hit.transform.GetComponent<IDamagable>();
			if(damageable != null)
			{
				//hit something that has functionality when hit
				damageable.damage(damage);
			}
		}
		//didnt hit
	}
}
