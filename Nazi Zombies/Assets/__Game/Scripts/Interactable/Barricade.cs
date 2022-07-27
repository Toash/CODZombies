using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Barricade : MonoBehaviour,IDamagable,IRepairable
{ 
	[SerializeField]
	private BarricadeStats stats;
	public int CurrentWood { get; private set; }

	public UnityEvent AtLeastOneBarricade;
	public UnityEvent NoBarricades;
	private void Awake()
	{
		CurrentWood = stats.MaxWood;
	}
	public void damage(int amount)
	{
		//Debug.Log("Barricade being damaged!");
		CurrentWood -= 1;
		if (CurrentWood <= 0) NoBarricades?.Invoke();
	}
	public void Repair()
	{
		CurrentWood += 1;
	}
	
}
