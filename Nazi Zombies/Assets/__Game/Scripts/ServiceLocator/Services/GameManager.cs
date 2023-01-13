using UnityEngine;
using System.Collections;


/// <summary>
/// Keeps track of zombies spawned. and changes round when no zombies left etc.  
/// </summary>
public class GameManager : MonoBehaviour
{
    public int CurrentZombies { get; private set; }
    private bool roundOngoing = false;

    private void Start() {
        ServLoc.Inst.Rounds.RoundChanged += RoundStarted;
    }
    private void Update() {
       
    }
    public void AddZombieCount() {
        CurrentZombies += 1;
    }
    public void RemoveZombieCount() {
        CurrentZombies -= 1;
    }

    /// <summary>
    /// Handles round starting behaviour
    /// </summary>
    private void RoundStarted()
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

