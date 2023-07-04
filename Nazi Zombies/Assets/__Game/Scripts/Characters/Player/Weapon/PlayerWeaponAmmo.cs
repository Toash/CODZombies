using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Player
{


    public class PlayerWeaponAmmo : MonoBehaviour
    {
        public class WeaponAmmoData
        {
            [ShowInInspector, PropertyOrder(-1)]
            public WeaponStats weapon { get; set; } //weapon to keep track of ammo
            public int magCount;
            public int reserveCount;

            public WeaponAmmoData(WeaponStats weapon, int magCount, int reserveCount)
            {
                this.weapon = weapon;
                this.magCount = magCount;
                this.reserveCount = reserveCount;
            }
        }
        [SerializeField]
        private PlayerWeaponShooter playerShooter;
        [SerializeField]
        private PlayerInventory playerInventory;

        /// <summary>
        /// Contains ammo data for each weapon.
        /// </summary>
        [ShowInInspector]
        private List<WeaponAmmoData> weaponAmmoList = new List<WeaponAmmoData>();

        private int currentMaxMagCount;
        private int currentReserveCount;
        private int currentMaxReserveCount;

        public delegate void Action();
        public Action Reload;


        private void OnEnable()
        {
            //every time weapon is added, should check if we have ammo data
            
            playerShooter.GunFireEvent += DecreaseAmmo;

        }
        private void Start()
        {
            playerInventory.weaponChanged += AddWeaponToAmmoList;
            playerInventory.weaponChanged += UpdateCount; // need to do in start, first equip is in awake.
        }
        private void OnDisable()
        {
            playerShooter.GunFireEvent -= DecreaseAmmo;
            playerInventory.weaponChanged -= UpdateCount;
            playerInventory.weaponChanged -= AddWeaponToAmmoList;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadWeapon(playerInventory.EquippedWeapon);
            }
        }

        /// <summary>
        /// Returns false if no ammo left in mag
        /// </summary>
        /// <returns></returns>
        public bool WeaponHasAmmoInMag(WeaponStats weapon)
        {
            if (FindAmmoDataFromWStats(weapon).magCount > 0) return true;
            return false;
        }

        public void ReloadWeapon(WeaponStats weapon)
        {
            if(FindAmmoDataFromWStats(weapon).magCount < weapon.MaxMagCount)
            {
                Debug.Log("Weapon: Reloading");
                int ammoToRemoveInReserve = weapon.MaxMagCount - FindAmmoDataFromWStats(weapon).magCount;
                FindAmmoDataFromWStats(weapon).magCount = currentMaxMagCount; // change the scriptable objects ammo
                FindAmmoDataFromWStats(weapon).reserveCount -= ammoToRemoveInReserve; // change the scriptable objects ammo
                return;
            }
            Debug.Log("Weapon: Already has max magazine size");
        }

        public void FillReserves(WeaponStats stats)
        {
            try
            {
                WeaponAmmoData data = FindAmmoDataFromWStats(stats);
                data.reserveCount = stats.MaxReserveCount;
            }
            catch(ArgumentNullException e)
            {
                Debug.LogError("Player: cannot fill reserve bceause player does not have weapon");
            }

        }

        /// <summary>
        /// Decrease ammo in mag
        /// </summary>
        public void DecreaseAmmo(WeaponStats weapon)
        {
            if (FindAmmoDataFromWStats(weapon).magCount <= 0) Debug.LogError("Player: mag ammo below zero");
            FindAmmoDataFromWStats(weapon).magCount--; // change the scriptable objects ammo
        }

        private void UpdateCount(WeaponStats weapon)
        {
            currentMaxMagCount = weapon.MaxMagCount;
            currentReserveCount = FindAmmoDataFromWStats(weapon).reserveCount;
            currentMaxReserveCount = weapon.MaxReserveCount;
        }

        /// <summary>
        /// Add a weapon to the ammo list with max mag and reserves, so we can keep track of the ammo.
        /// </summary>
        /// <param name="weapon"></param>
        private void AddWeaponToAmmoList(WeaponStats weapon)
        {
            bool weaponAmmoInList = weaponAmmoList.Any(WeaponAmmoData => WeaponAmmoData.weapon == weapon);
            //Check if it is already in the list
            if (weaponAmmoInList)
            {
                Debug.Log("Weapon, already has weapon ammo data.");
                return;
            }

            //Add to list
            Debug.Log("Weapon, added weapon to weapon ammo data.");
            weaponAmmoList.Add(new WeaponAmmoData(weapon, weapon.MaxMagCount,weapon.MaxReserveCount));
        }

        public WeaponAmmoData FindAmmoDataFromWStats(WeaponStats weapon)
        {
            try
            {
                WeaponAmmoData ammoData = weaponAmmoList.FirstOrDefault(WeaponAmmoData => WeaponAmmoData.weapon == weapon);
                //Debug.Log($"Weapon: Weapon ammo data found for {ammoData.weapon}");
                return ammoData;
            }
            catch(ArgumentNullException e)
            {
                //Debug.Log("Weapon Ammo list not found!");
                return null;
            }
        }
    }
}
