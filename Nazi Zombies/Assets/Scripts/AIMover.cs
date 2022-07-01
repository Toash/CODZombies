using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMover : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    public FloatVariable moveSpeed;

    private NavMeshAgent agent;

	void Awake()
	{
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed.Value;
	}
	void Update()
    {
		if (target != null)
		{
            this.agent.SetDestination(target.transform.position);
        }
    }
}
