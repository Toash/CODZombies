using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//template for item data
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
	public string weaponName;

	public int damage;
	public bool automatic;//false is semi
	public float fireRate;

	public int magSize;
	public int reserveSize;

	public GameObject model;
}

