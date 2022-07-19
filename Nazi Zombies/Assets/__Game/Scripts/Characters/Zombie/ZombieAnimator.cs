using UnityEngine;

namespace AI.Zombie
{
	[RequireComponent(typeof(ZombieMovement))]
	public class ZombieAnimator : BaseAnimator
	{
		private ZombieMovement movement;
		
		SetAnimBoolDelegate moving;

		protected override void Awake()
		{
			base.Awake();
			movement = this.GetComponent<ZombieMovement>();
			moving = SetMovingBool;
		}
		private void Update()
		{
			if (movement.isMoving)
			{
				moving(true);
			}
			else
			{
				moving(false);
			}

		}
		private void SetMovingBool(bool a)
		{
			anim.SetBool("isMoving", a);
		}

		
	}
}

