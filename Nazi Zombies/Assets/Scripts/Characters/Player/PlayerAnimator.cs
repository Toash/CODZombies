using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(PlayerWeaponShooter))]
	[RequireComponent(typeof(PlayerMovement))]
	public class PlayerAnimator : MonoBehaviour
	{
		private Animator anim;
		private PlayerWeaponShooter shooter;

		void Awake()
		{
			anim = this.GetComponent<Animator>();
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

