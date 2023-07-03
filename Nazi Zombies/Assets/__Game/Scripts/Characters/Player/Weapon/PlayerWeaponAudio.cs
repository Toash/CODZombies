using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Player
{
    /// <summary>
    /// Handles playing the gun audio by subscribing to the GunFire event in PlayerWeaponShooter
    /// </summary>
    public class PlayerWeaponAudio : MonoBehaviour
    {
        [SerializeField]
        private PlayerWeaponShooter weaponShooter;

        [SerializeField,Title("AudioSource to play gun sound")]
        private AudioSource audioSource;

        private void OnEnable()
        {
            weaponShooter.GunFireEvent += PlayGunAudio;
        }
        private void OnDisable()
        {
            weaponShooter.GunFireEvent -= PlayGunAudio;
        }

        private void PlayGunAudio(WeaponStats weapon)
        {
            audioSource.PlayOneShot(weapon.ShootSound);
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

