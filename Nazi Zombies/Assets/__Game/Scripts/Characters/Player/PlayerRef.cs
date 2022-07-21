using UnityEngine;

//Gives references to player to other scripts
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

