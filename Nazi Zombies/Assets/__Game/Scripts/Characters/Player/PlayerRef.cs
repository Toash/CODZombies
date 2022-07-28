using UnityEngine;

//Gives references to player to other scripts
//When any other scripts need reference to player, they should do so by interfacing through here. 
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

