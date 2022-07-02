using UnityEngine;
using UnityEngine.Events;


public class GameEventListener : MonoBehaviour
{
	
	public GameEvent Event; //The event that this listener listens too.

	public UnityEvent Response; //

	//OnEnable() called when object is loaded
	private void OnEnable()
	{ 
		//Adds this Listener to the list in the GameEvent
		Event.RegisterListener(this); 
	}

	//OnDisable() called when the ScriptableObject is out of scope
	private void OnDisable()
	{
		//Removes this Listener to the list in the GameEvent
		Event.UnregisterListener(this); 
	}

	public void OnEventRaised()
	{ 
		Response.Invoke(); 
	}
}
