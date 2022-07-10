using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(PlayerWeaponShooter))]
	[RequireComponent(typeof(PlayerMovement))]
	public class PlayerAnimator : BaseAnimator
	{

		private PlayerWeaponShooter shooter;

		protected override void Awake()
		{
			base.Awake();
			shooter = this.GetComponent<PlayerWeaponShooter>();
		}
		public void OnEnable()
		{
			shooter.gunShoot += ActivateShootTrigger;
		}
		public void OnDisable()
		{
			shooter.gunShoot -= ActivateShootTrigger;
		}

		private void ActivateShootTrigger()
		{
			anim.SetTrigger("Shoot");
		}
	}
}

