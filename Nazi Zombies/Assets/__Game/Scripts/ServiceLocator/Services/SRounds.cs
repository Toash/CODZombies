using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SRounds : MonoBehaviour
{
    [SerializeField, Required("Dependency")]
    private SSpawner spawner;
    [SerializeField]
    private AudioSource roundChangeSound;

    [ReadOnly]
    public int CurrentRound { get; private set; }
    [ReadOnly]
    public bool RoundChanging { get; private set; }

    public delegate void Action();
    public Action roundChanged;

    private readonly float ROUND_CHANGE_TIME = 2;

    private void Update()
    {
        if (spawner.ZombiesToSpawn <= 0)
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
        roundChanged?.Invoke();
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
