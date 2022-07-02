using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	//The GameEventListeners that are listening to THIS event
	private List<GameEventListener> listeners = new List<GameEventListener>();

	//loops through listeners and invokes the response (UnityEvent)
	//loops backwards so if an event deletes a listener, there isn't an OutOfBounds exception
	public void Raise()
	{
		for(int i = listeners.Count - 1; i >= 0; i--)
		{
			listeners[i].OnEventRaised();
		}
	}

	public void RegisterListener(GameEventListener listener)
	{ 
		listeners.Add(listener); 
	}

	public void UnregisterListener(GameEventListener listener)
	{ 
		listeners.Remove(listener); 
	}
}
