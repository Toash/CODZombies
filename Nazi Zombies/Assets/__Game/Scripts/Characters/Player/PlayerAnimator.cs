using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(PlayerWeaponHandler))]
	[RequireComponent(typeof(PlayerMovement))]
	public class PlayerAnimator : BaseAnimator
	{

		private PlayerWeaponHandler shooter;

		private SetAnimBoolDelegate aiming;

		protected override void Awake()
		{
			base.Awake();
			shooter = this.GetComponent<PlayerWeaponHandler>();
			aiming = SetAimingBool;
		}
		public void OnEnable()
		{
			shooter.gunShootEvent += ActivateShootTrigger;
			shooter.isAimingEvent += SetAimingBool;
		}
		public void OnDisable()
		{
			shooter.gunShootEvent -= ActivateShootTrigger;
			shooter.isAimingEvent -= SetAimingBool;
		}

		private void ActivateShootTrigger()
		{
			anim.SetTrigger("Shoot");
		}
		private void SetAimingBool(bool a)
		{
			anim.SetBool("isAiming", a);
		}
	}
}

