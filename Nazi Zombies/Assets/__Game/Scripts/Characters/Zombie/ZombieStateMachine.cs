using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//handles what the zombie is currently doing
//other classes can see what zombie is doing and decide
public class ZombieStateMachine : MonoBehaviour
{
	public State CurrentState { get; private set; }

	public enum State
	{
		PURSUE,
		ATTACK,
	}

	public State GetState()
	{
		return CurrentState;
	}

	public void ChangeState(State state)
	{
		CurrentState = state;
	}
}
