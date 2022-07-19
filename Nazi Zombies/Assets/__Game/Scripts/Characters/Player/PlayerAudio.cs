using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudio : BaseAudio
    {
		public void PlayWeaponSound(Weapon weapon)
        {
            audioSource.PlayOneShot(weapon.shootSound);
        }
    }
}

