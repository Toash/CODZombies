using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
	public float Value;

	public void SetValue(float Value)
	{
		this.Value = Value;
	}

	public void SetValue(FloatVariable value)
	{
		this.Value = value.Value;
	}

	public void ApplyChange(float amount)
	{
		this.Value += amount;
	}

	public void ApplyChange(FloatVariable amount)
	{
		this.Value += amount.Value;
	}
}
