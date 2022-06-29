using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject
{
	public int Value;

	public void SetValue(int value)
	{
		this.Value = value;
	}
	public void SetValue(IntVariable value)
	{
		this.Value = value.Value;
	}

	public void ApplyChange(int amount)
	{
		this.Value += amount;
	}
	public void ApplyChange(IntVariable amount)
	{
		this.Value += amount.Value;
	}
}
