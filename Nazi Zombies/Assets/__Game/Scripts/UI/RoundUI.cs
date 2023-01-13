using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundUI : MonoBehaviour
{
    public TMP_Text roundText;

    private void Update()
    {
        roundText.text = "Round " + ServLoc.Inst.Rounds.CurrentRound.ToString();
    }
}
