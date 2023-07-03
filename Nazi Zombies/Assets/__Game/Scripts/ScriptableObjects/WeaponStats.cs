using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Contains read only data for weapon
/// </summary>
[CreateAssetMenu]
public class WeaponStats : ScriptableObject
{
	public int Damage;
	public float Range;
	public float FireRate;
	public bool Automatic;
	public float ReloadTime;

	[Title("AMMO")]
	public int MaxMagCount;
	public int MaxReserveCount;

	[Tooltip("Vertical Camera Recoil")]
	public float VCamRecoil;
	[Tooltip("Horizontal Camera Recoil")]
	public float HCamRecoil;
	[Tooltip("Force used when colliding with Rigidbodies")]
	public float PhysicsForce;

	[Title("DISPLAY")]
	public GameObject Model;
	[Tooltip("How far the gun kicks back when firing")]
	public float VisualRecoil;

	[Header("Audio")]
	public AudioClip ShootSound;



}
