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
        ZombiesInRoundCountText.text = ServLoc.Inst.GameManager.CurrentZombies.ToString();
    }
}
