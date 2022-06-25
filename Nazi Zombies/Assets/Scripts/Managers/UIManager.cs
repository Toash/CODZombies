using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	public TMP_Text weaponNameText;


	public static UIManager Instance { get; private set; }
	private void Awake()
	{
		Instance = this;
	}


}
