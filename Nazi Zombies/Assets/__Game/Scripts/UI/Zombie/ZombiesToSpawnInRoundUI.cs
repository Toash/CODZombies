using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombiesToSpawnInRoundUI : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.text = $"Zombies to spawn in round: + {ServLoc.Inst.ZombieSpawner.ZombiesLeftToSpawnInCurrentRound}";
    }
}
