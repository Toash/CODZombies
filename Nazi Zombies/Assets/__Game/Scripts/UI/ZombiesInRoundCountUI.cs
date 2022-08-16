using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombiesInRoundCountUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text ZombiesInRoundCountText;

    private void Update()
    {
        ZombiesInRoundCountText.text = ServiceLocator.Instance.Spawner.ZombiesToSpawn.ToString();
    }
}
