using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class ZombieMovement : MonoBehaviour
    {
        [SerializeField]
        private GameObject target;

        public FloatVariable moveSpeed;

        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed.Value;
        }
        private void Update()
        {
            if (target != null)
            {
                this.agent.SetDestination(target.transform.position);
            }
        }
    }
}

