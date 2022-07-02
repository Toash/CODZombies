using UnityEngine;

public class Health : MonoBehaviour,IDamagable
{
	[Header("Stats")]
	public IntVariable maxHealth;
	[Header("Events")]
	public GameEvent DeathEvent;
	public GameEvent DamageEvent;

	private int health;

	private void Awake()
	{
		health = maxHealth.Value;
	}
	public void damage(int amount)
	{
		if(DamageEvent!=null){DamageEvent.Raise();}
		if (health <= 0) { die(); }
	}
	private void die()
	{
		if (DeathEvent != null){DeathEvent.Raise();}
	}



}
