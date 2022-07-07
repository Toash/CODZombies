using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CharacterHealth : MonoBehaviour, IDamagable
{
	[Header("Stats")]
	public IntVariable startingHealth;
	public IntVariable lowHealthPercentThreshold;

	[Header("Unity Events")]
	public UnityEvent DamagedEvent;
	public UnityEvent DeathEvent;

	[Header("Thresholds")]
	public UnityEvent LowHealthEvent;

	private int currentHealth;

	private bool lowHealth
	{
		get
		{
			if (lowHealthPercentThreshold != null)
			{
				return ((currentHealth/startingHealth.Value)*100) >= lowHealthPercentThreshold.Value;
			}
			return false;
		}
	}
	private bool noHealth
	{
		get
		{
			return currentHealth <= 0;
		}
	}

	private void Awake()
	{
		currentHealth = startingHealth.Value;
	}
	public void damage(int amount)
	{
		currentHealth -= amount;
		// ?. is null conditional operator, will only do if not null.
		DamagedEvent?.Invoke();
		if (this.lowHealth)
		{
			LowHealthEvent?.Invoke();
		}
		if (this.noHealth)
		{
			DeathEvent?.Invoke();
		}
	}
}
