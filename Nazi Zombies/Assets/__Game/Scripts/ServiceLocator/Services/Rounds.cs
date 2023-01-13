using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Changing rounds
/// </summary>
public class Rounds : MonoBehaviour
{
    [SerializeField] private AudioSource roundChangeSound;

    public int CurrentRound { get; private set; }
    public bool RoundChanging { get; private set; }

    public delegate void Action();
    public Action RoundChanged;

    private readonly float ROUND_CHANGE_TIME = 2;

    //Game Starts
    private void Start()
    {
        ChangeRound(1);
    }
    private void Update()
    {
        if (ServLoc.Inst.ZombieSpawner.ZombiesLeftToSpawnInCurrentRound <= 0)
        {
            ChangeRound(CurrentRound + 1);
        }
    }

    private void ChangeRound(int round)
    {
        if (!RoundChanging)
        {
            StartCoroutine(ChangeRoundRoutine(round));
        }
    }
    private IEnumerator ChangeRoundRoutine(int round)
    {
        // Round changing behaviour

        RoundChanging = true;
        RoundBeginSound();
        RoundChanged?.Invoke();

        yield return new WaitForSeconds(ROUND_CHANGE_TIME);

        // Exit
        CurrentRound = round;
        RoundChanging = false;
    }
    private void RoundBeginSound()
    {
        if (roundChangeSound == null) return;
        roundChangeSound.Play();
    }
}
