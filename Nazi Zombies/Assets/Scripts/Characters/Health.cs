using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Health : MonoBehaviour,IDamagable
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
		health -= amount;
		if(DamageEvent!=null){DamageEvent.Raise();}
		if (health <= 0) 
		{
			this.die(); 
		}
	}
	protected abstract void die();




}
