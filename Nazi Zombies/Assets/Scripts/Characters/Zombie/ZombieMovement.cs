using UnityEngine;
using UnityEngine.AI;

namespace AI.Zombie
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;

        public FloatVariable moveSpeed;

        private NavMeshAgent agent;

        public bool isMoving { get { return !agent.isStopped; } }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed.Value;
        }
        private void Update()
        {
            //hasPath-This property will be true if the agent has a path calculated to the desired destination and false otherwise.
            if (target != null&&!agent.hasPath)
            {
                this.agent.SetDestination(target.transform.position);
            }
        }
	}
}

