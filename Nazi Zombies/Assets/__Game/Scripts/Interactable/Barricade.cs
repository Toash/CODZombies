using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Barricade : MonoBehaviour,IDamagable
{ 
	[SerializeField]
	private BarricadeStats stats;
	public int CurrentWood { get; private set; }
	[InfoBox("When there is atleast 1 barricade/wood")]
	public UnityEvent HaveBarricades;
	[InfoBox("When there are no more barricades/wood left")]
	public UnityEvent DontHaveBarricades;
	private void Awake()
	{
		CurrentWood = stats.MaxWood;
	}
	public void damage(int amount)
	{
		//Debug.Log("Barricade being damaged!");
		CurrentWood -= 1;
		if (CurrentWood <= 0) DontHaveBarricades?.Invoke();
	}
}
