using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRounds : MonoBehaviour
{

    public int CurrentRound { get; private set; }
    public bool RoundChanging { get; private set; }

    public delegate void Action();
    public Action roundChanged;



    private void IncreaseRound()
    {
        CurrentRound += 1;
        roundChanged?.Invoke();
    }
}
