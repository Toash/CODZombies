using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieMovement : MonoBehaviour
{
    public FloatVariable zombieMoveSpeed;

    private NavMeshAgent agent;

	void Awake()
	{
        agent = GetComponent<NavMeshAgent>();
	}
	void Update()
    {
        goToPlayer();
    }

    public void goToPlayer()
    {
        //this.agent.SetDestination(GameManager.playerPosition);
    }
}
