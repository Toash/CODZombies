using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerWeaponRecoil : MonoBehaviour
    {
        [SerializeField]
        private PlayerCamera playerCam;
        [SerializeField]
        private PlayerWeaponShooter shooter;


        private void OnEnable()
        {
            shooter.GunFireEvent += WeaponFired;
        }

        private void OnDisable()
        {
            shooter.GunFireEvent -= WeaponFired;
        }
        private void WeaponFired(Weapon weapon)
        {
            ApplyRecoil(weapon.HCamRecoil, weapon.VCamRecoil);
        }
        public void ApplyRecoil(float horizRecoil, float vertRecoil)
        {
            playerCam.ApplyCameraMovement(new Vector2(horizRecoil, vertRecoil));
        }
    }
}
