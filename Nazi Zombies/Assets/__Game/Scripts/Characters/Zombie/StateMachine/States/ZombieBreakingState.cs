using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace AI.Zombie
{
	/// <summary>
	/// State for Breaking windows
	/// </summary>
	public class ZombieBreakingState : ZombieBaseState
	{
		[ShowInInspector, ReadOnly]
		private IZombieBreakable currentBreakable;

		[SerializeField]
		private float breakSpeed = 1f;

		private float timer;
		private bool canBreak(ZombieStateManager manager)
		{
			return breakSpeed <= timer;
		}
		private bool barricadeBroken(IZombieBreakable barricade)
		{
			return barricade.Broken;
		}
		public override void EnterState(ZombieStateManager manager)
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

			foreach (Collider col in colliders)
			{
				if (col.GetComponent<IZombieBreakable>() != null)
				{
					currentBreakable = col.GetComponent<IZombieBreakable>();
					StopZombie(manager);
				}
			}
		}

		public override void FixedUpdateState(ZombieStateManager manager)
		{

		}

		public override void LateUpdateState(ZombieStateManager manager)
		{

		}

		public override void TriggerEnter(ZombieStateManager manager, Collider other)
		{

		}

		public override void TriggerExit(ZombieStateManager manager, Collider other)
		{

		}

		public override void TriggerStay(ZombieStateManager manager, Collider other)
		{

		}

		public override void UpdateState(ZombieStateManager manager)
		{
			timer += Time.deltaTime;
			//Debug.Log("In breaking state");
			if (!barricadeBroken(currentBreakable) && canBreak(manager))
			{
				Break(currentBreakable);
			}
			else if (barricadeBroken(currentBreakable))
			{
				manager.SwitchState(manager.ChasingState);
			}
			else if (!canBreak(manager))
            {
				//Cooldown
            }
			else
			{
				//barricade is null
				Debug.LogError("BARRICADE IS NULLLLLLLLLLL");
			}
		}

		private void Break(IZombieBreakable thingToBreak)
		{
			thingToBreak.Break();
			timer = 0;
		}
	}
}