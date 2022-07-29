using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
	public int Damage;
	public float Range;
	public LayerMask WhatToHit;
	public QueryTriggerInteraction ShouldHitTriggers;
	public GameObject BulletHole;

	public float FireRate;
	public bool Automatic;
	public float ReloadTime;
	[Title("DISPLAY")]
	public GameObject model;
	public float VisualRecoil;

	[Header("Audio")]
	public AudioClip shootSound;

	private GameObject weaponModel;
	

}
