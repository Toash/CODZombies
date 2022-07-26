using UnityEngine;

public class Barricade : MonoBehaviour,IDamagable
{
	private int wood;

	public void damage(int amount)
	{
		Debug.Log("Barricade being damaged!");
	}
}
