using UnityEngine;
using UnityEngine.AI;

namespace AI.Zombie
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieMovement : MonoBehaviour
    {
        [SerializeField]
        private ZombieStats stats;

        private NavMeshAgent agent;

        public bool isMoving { get { return !agent.isStopped; } }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = stats.Speed;
        }
        private void Update()
        {
            this.agent.SetDestination(PlayerRef.Instance.PlayerPosition());
        }
	}
}

