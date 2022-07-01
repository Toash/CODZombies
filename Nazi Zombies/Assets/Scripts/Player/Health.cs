using UnityEngine;

public class Health : MonoBehaviour,IDamagable
{
	public IntVariable maxHealth;

	private int health;

	private void Awake()
	{
		health = maxHealth.Value;
	}
	public void damage(int amount)
	{
		if (health <= 0) { die(); }
	}
	private void die()
	{
		//send event
	}



}
