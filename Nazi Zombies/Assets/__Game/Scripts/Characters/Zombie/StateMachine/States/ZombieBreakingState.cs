using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	/// <summary>
	/// State for Breaking barricades 
	/// </summary>
	public class ZombieBreakingState : ZombieBaseState
	{
		[ShowInInspector, ReadOnly]
		private IZombieBreakable currentBreakable;

		[SerializeField] private float breakSpeed = 1f;
		private float timer;

		private bool breakingCooldownUp(Zombie manager)
		{
			return breakSpeed <= timer;
		}
		private bool isBarricadeBroken(IZombieBreakable barricade)
		{
			return barricade.Broken;
		}
		 
		public override void EnterState(Zombie manager) {
			if (GetNearestBarricade(manager))
			{
                base.StopZombie(manager);
            }
			else
			{
				Debug.LogError("Barricade magically disapeared?");
				manager.SwitchState(manager.ChasingState);
			}
        }

        private bool GetNearestBarricade(Zombie manager) {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

            foreach (Collider col in colliders) {
				// Just get the first one
                if (col.GetComponent<IZombieBreakable>() != null) {
					currentBreakable = col.GetComponent<IZombieBreakable>();
					return true;
                }
            }
			return false;
        }

        public override void FixedUpdateState(Zombie manager)
		{

		}

		public override void LateUpdateState(Zombie manager)
		{

		}

		public override void TriggerEnter(Zombie manager, Collider other)
		{

		}

		public override void TriggerExit(Zombie manager, Collider other)
		{

		}

		public override void TriggerStay(Zombie manager, Collider other)
		{

		}

		public override void UpdateState(Zombie manager)
		{
			timer += Time.deltaTime;
			if (breakingCooldownUp(manager))
			{
				if (!isBarricadeBroken(currentBreakable) && breakingCooldownUp(manager))
				{
					Break(currentBreakable);
					ResetTimer();
				}
				//Barricade broke, go into chase state
				else if (isBarricadeBroken(currentBreakable))
				{
					manager.SwitchState(manager.ChasingState);
				}
				else
				{
					Debug.LogError("Barricade is null");
				}
			}
		}
		private void Break (IZombieBreakable thingToBreak)
		{
			thingToBreak.Break();
		}
		private void ResetTimer()
		{
			timer = 0;
		}
	}
}