using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
	public int Damage;
	public float Range;
	public float VerticalRecoil;
	public float HorizontalRecoil;
	[InfoBox("Force used when colliding with Rigidbodies")]
	public float DamageForce;

	public float FireRate;
	public bool Automatic;
	public float ReloadTime;

	[Title("DISPLAY")]
	public GameObject Model;
	[InfoBox("How far the gun kicks back when firing")]
	public float VisualRecoil;

	[Header("Audio")]
	public AudioClip ShootSound;
}
