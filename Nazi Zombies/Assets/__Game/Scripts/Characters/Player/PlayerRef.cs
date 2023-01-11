using UnityEngine;

/// <summary>
/// When any other scripts need reference to player,
/// they should do so by interfacing through here. 
/// </summary>

public class PlayerRef : MonoBehaviour
{
	public static PlayerRef Instance { get; private set; }
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Debug.LogError("Multiplle players???");
		}
	}
	public Vector3 PlayerPosition()
	{
		return transform.position;
	}
}

