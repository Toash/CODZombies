using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class BaseHealth : MonoBehaviour,IDamagable
{
	[Header("Stats")]
	public IntVariable startingHealth;
	//[Header("Game Events")]
	//public GameEvent DamageEvent;
	//public GameEvent DeathEvent;
	[Header("Unity Events")]
	public UnityEvent DamageEvent;
	public UnityEvent DeathEvent;

	private int currentHealth;

	private void Awake()
	{
		currentHealth = startingHealth.Value;
	}
	public void damage(int amount)
	{
		currentHealth -= amount;
		// ?. is null conditional operator, will only do if not null.
		DamageEvent?.Invoke();
		if (currentHealth <= 0) 
		{
			DeathEvent?.Invoke();
			die();
		}
	}
	protected abstract void die();
}
