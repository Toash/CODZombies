using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Contains methods for changing rounds
/// </summary>
public class Rounds : MonoBehaviour
{
    public int CurrentRound { get; private set; } = 0;
    public bool RoundChanging { get; private set; } = false;

    public delegate void RoundChange(int round);
    public RoundChange RoundChanged;

    [SerializeField]
    private  float roundChangeTime = 2f;

    // As soon as round changes this should be false. Because need to spawn zombies
    private bool allZombiesInRoundSpawned { get { return ServLoc.Inst.ZombieSpawner.ZombiesLeftToSpawnInCurrentRound <= 0; } }
    private bool noZombiesActive { get { return ServLoc.Inst.GameManager.ZombieCount <= 0; } }
    
    private void Update()
    {
        if (allZombiesInRoundSpawned && noZombiesActive)
        {
            ChangeRound(CurrentRound + 1);
            Debug.Log("test");
        }
    }

    private void ChangeRound(int round)
    {
        if (!RoundChanging)
        {
            StartCoroutine(ChangeRoundRoutine(round));
            Debug.Log("Starting coroutine to change round to " + round);
        }
    }
    private IEnumerator ChangeRoundRoutine(int round)
    {
        // Round changing behaviour
        RoundChanging = true;
        RoundChanged.Invoke(round); // passing the new round to delegate

        yield return new WaitForSeconds(roundChangeTime); // pause a bit before new round

        // Exit, new round begins
        Debug.Log("Round changed");
        CurrentRound = round;
        RoundChanging = false;
    }
}
