using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
	[Header("Stats")]
	public int damage;
	public float fireRate;
	public float range;
	public bool automatic;
	[Header("Visuals")]
	public GameObject model;

}
