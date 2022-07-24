using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieStateManager : MonoBehaviour
{
	[ShowInInspector, ReadOnly]
	private ZombieBaseState currentState;

	[Title("Dependencies")]
	public NavMeshAgent agent;
	public GameObject interactor;//interactor should be in this class

	[ShowIf("interactor")]
	public Rigidbody rb;

	public ZombieStats stats;

	[Title("States that the StateManager Uses")]
	public ZombieChasingState ChasingState;
	public ZombieAttackingState AttackingState;

	IZombieUpdatable currentUpdateable;

	private void Start()
	{
		SwitchState(ChasingState);
	}
	//what trigger is being used? 
	private void OnTriggerEnter(Collision other)
	{
		IZombieTriggerEnter triggerEnter = currentState.GetComponent<IZombieTriggerEnter>();
		if (triggerEnter != null)
		{
			triggerEnter.OnTriggerEnter(this, other);
		}
		
	}
	private void OnTriggerStay(Collision other)
	{
		IZombieTriggerStay stay = currentState.GetComponent<IZombieTriggerStay>();
		if (stay != null)
		{
			stay.OnTriggerStay(this, other);
		}
		
	}
	private void Update()
	{
		currentUpdateable?.UpdateState(this);
	}
	public void SwitchState(ZombieBaseState state)
	{
		currentState = state;
		IZombieEnterable enter = currentState.GetComponent<IZombieEnterable>();
		if (enter != null)
		{
			enter.EnterState(this);
		}
		if (currentState.GetComponent<IZombieUpdatable>() != null)
		{
			currentUpdateable = currentState.GetComponent<IZombieUpdatable>();
		}
	}
}
