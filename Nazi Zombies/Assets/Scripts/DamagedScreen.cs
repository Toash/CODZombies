using UnityEngine;
using UnityEngine.UI;

public class DamagedScreen : MonoBehaviour
{
	private RawImage damageScreen;

	void Awake()
	{
		damageScreen = this.GetComponent<RawImage>();
	}

	public void ShowDamageScreen()
	{
		damageScreen.color = new Color(255, 0, 0, 50);
	}

}
