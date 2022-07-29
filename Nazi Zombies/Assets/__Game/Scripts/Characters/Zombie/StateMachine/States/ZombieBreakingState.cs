﻿using System.Collections;
using UnityEngine;

namespace AI.Zombie
{
	/// <summary>
	/// State for Breaking windows
	/// </summary>
	public class ZombieBreakingState : ZombieBaseState
	{
		private IZombieBreakable currentBreakable;

		private float timer;
		private void Update()
		{
			timer += Time.deltaTime;
		}
		private bool canBreak(ZombieStateManager manager)
		{
			return manager.stats.BarricadeBreakSpeed <= timer;
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
			Debug.Log("In breaking state");
			if (!barricadeBroken(currentBreakable) && canBreak(manager))
			{
				Break(currentBreakable);
			}
			else if (barricadeBroken(currentBreakable))
			{
				manager.SwitchState(manager.ChasingState);
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