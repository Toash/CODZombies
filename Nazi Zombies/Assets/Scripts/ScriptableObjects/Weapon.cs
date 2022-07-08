using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
	[Header("Stats")]
	public int damage;
	public float fireRate;
	public float range;
	public bool automatic;
	public float reloadTime;
	[Header("Visuals")]
	public GameObject model;
	[Header("Audio")]
	public AudioClip shootSound;
	public float shootSoundVolume;

	private GameObject weaponModel;

	public void DisplayWeapon(Transform parent)
	{
		weaponModel = Instantiate(model, parent);
	}
	public void DeleteDisplayWeapon()
	{
		Destroy(weaponModel.gameObject);
	}
	

}
