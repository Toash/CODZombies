using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class BaseHealth : MonoBehaviour, IDamagable
{
	[Header("Base")]
	[Header("Stats")]
	public IntVariable startingHealth;

	[Header("Unity Events")]
	public UnityEvent DamagedEvent;
	public UnityEvent DeathEvent;

	protected int currentHealth;

	protected bool noHealth
	{
		get
		{
			return currentHealth <= 0;
		}
	}

	protected void Awake()
	{
		currentHealth = startingHealth.Value;
	}
	public virtual void damage(int amount)
	{
		currentHealth -= amount;
		DamagedEvent?.Invoke();
		if (this.noHealth) DeathEvent?.Invoke();
	}
}
