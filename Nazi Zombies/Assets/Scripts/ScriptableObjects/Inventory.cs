using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
	//should equipping be handled in this class?
	public Weapon[] weapons = new Weapon[2];

	public void AddWeapon(Weapon weapon)
	{
		//check if the list is full, if so override current weapon
	}
}
