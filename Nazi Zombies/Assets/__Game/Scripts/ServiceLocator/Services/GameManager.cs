using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;


/// <summary>
/// Keeps track of zombies spawned. and changes round when no zombies left etc.  
/// </summary>
public class GameManager : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    public int ZombieCount { get; private set; }
    private bool roundOngoing = false;

    private void Start() {
        ServLoc.Inst.Rounds.RoundChanged += RoundStarted;
    }
    private void Update() {
       
    }
    public void AddZombieCount() {
        ZombieCount += 1;
    }
    public void RemoveZombieCount() {
        ZombieCount -= 1;
    }

    /// <summary>
    /// Handles round starting behaviour
    /// </summary>
    private void RoundStarted(int round)
    {
        roundOngoing = true;
    }
    /// <summary>
    /// Handles round ending behaviour
    /// </summary>
    private void RoundEnded()
    {
        roundOngoing = false;
    }


    
}

