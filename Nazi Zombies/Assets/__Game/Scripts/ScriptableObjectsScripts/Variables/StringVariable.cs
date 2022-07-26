using UnityEngine;

[CreateAssetMenu(menuName = "Variables/String Variable")]
public class StringVariable : ScriptableObject
{
	public string Value;

	public void SetValue(string value)
	{
		this.Value = value;
	}
	public void SetValue(StringVariable value)
	{
		this.Value = value.Value;
	}
}
