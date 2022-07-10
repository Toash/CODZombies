using UnityEngine;

namespace AI.Zombie
{
	[RequireComponent(typeof(ZombieMovement))]
	public class ZombieAnimator : BaseAnimator
	{
		private ZombieMovement movement;
		private delegate void SetAnimBoolDelegate(bool a);
		SetAnimBoolDelegate animSetisMoving;

		protected override void Awake()
		{
			base.Awake();
			movement = this.GetComponent<ZombieMovement>();
			animSetisMoving = SetMovingBool;
		}
		private void Update()
		{
			if (movement.isMoving)
			{
				animSetisMoving(true);
			}
			else
			{
				animSetisMoving(false);
			}

		}
		private void SetMovingBool(bool a)
		{
			anim.SetBool("isMoving", a);
		}

		
	}
}

