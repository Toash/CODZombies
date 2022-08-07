using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
	public int Damage;
	public float Range;

	public float FireRate;
	public bool Automatic;
	public float ReloadTime;
	[Title("DISPLAY")]
	public GameObject Model;
	public float VisualRecoil;

	[Header("Audio")]
	public AudioClip ShootSound;
}
