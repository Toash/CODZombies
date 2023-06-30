using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class DestroyNavMesh : MonoBehaviour
{
    NavMeshAgent agent;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


}
