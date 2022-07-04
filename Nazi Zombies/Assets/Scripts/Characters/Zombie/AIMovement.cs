using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIMovement : MonoBehaviour
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
}

