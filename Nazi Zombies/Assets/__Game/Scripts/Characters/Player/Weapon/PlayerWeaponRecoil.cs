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
        private void WeaponFired(WeaponStats weapon)
        {
            ApplyRecoil(weapon.HCamRecoil, weapon.VCamRecoil);
        }
        public void ApplyRecoil(float horizRecoil, float vertRecoil)
        {
            float vertActual = Random.Range(vertRecoil / 2, vertRecoil);
            float horizActual = Random.Range(-horizRecoil, -vertRecoil);
            playerCam.ApplyCameraMovement(new Vector2(horizRecoil, vertRecoil));
        }
    }
}
