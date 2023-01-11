using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Rounds : MonoBehaviour
{
    [SerializeField] private AudioSource roundChangeSound;

    public int CurrentRound { get; private set; }
    public bool RoundChanging { get; private set; }

    public delegate void Action();
    public Action RoundChanged;

    private readonly float ROUND_CHANGE_TIME = 2;

    private void Update()
    {
        if (ServLoc.I.Spawner.ZombiesToSpawn <= 0)
        {
            ChangeRound();
        }
    }

    private void ChangeRound()
    {
        StartCoroutine(ChangeRoundRoutine());
    }
    private IEnumerator ChangeRoundRoutine()
    {
        // Round changing behaviour 
        RoundChanging = true;
        RoundBeginSound();
        RoundChanged?.Invoke();
        yield return new WaitForSeconds(ROUND_CHANGE_TIME);
        // Exit
        CurrentRound += 1;
        RoundChanging = false;
    }
    private void RoundBeginSound()
    {
        if (roundChangeSound == null) return;
        roundChangeSound.Play();
    }
}
