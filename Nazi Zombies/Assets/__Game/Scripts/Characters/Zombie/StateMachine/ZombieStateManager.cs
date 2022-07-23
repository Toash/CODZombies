using UnityEngine;
using UnityEngine.AI;

//manages the different zombie states
[RequireComponent(typeof(NavMeshAgent))]
public class ZombieStateManager : MonoBehaviour
{
	public NavMeshAgent agent { get; private set; }

	private ZombieBaseState currentState;

	public ZombieMovingState MovingState = new ZombieMovingState();
	public ZombieAttackingState AttackingState = new ZombieAttackingState();

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}
	private void Start()
	{
		SwitchState(MovingState);
	}
	private void OnTriggerEnter(Collision other)
	{
		currentState.OnTriggerEnter(this,other);
	}
	private void Update()
	{
		currentState.UpdateState(this);
	}
	public void SwitchState(ZombieBaseState state)
	{
		currentState = state;
		state.EnterState(this);
	}
}
