using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

// should barricades be the same as interactables?
//
//- You dont use raycast to select them, only triggers.
//- The player can hold down e to repair, other interactables are only one time events.
//- Barricades differ specifically in selecting them and "interacting" with them.
//
public class Barricade : MonoBehaviour, IDamagable, IPlayerInteractable
{
    [SerializeField]
    private BarricadeStats stats;
    public int CurrentWood { get; private set; }

    public UnityEvent AtLeastOneBarricade;
    public UnityEvent NoBarricades;

    private void Awake()
    {
        CurrentWood = stats.MaxWood;
    }
    //Can barricades be damaged by the player????
    public void damage(int amount)
    {
        //Debug.Log("Barricade being damaged!");
        CurrentWood -= 1;
        if (CurrentWood <= 0) NoBarricades?.Invoke();
    }
    public void PlayerInteract()
    {
        CurrentWood += 1;
    }

	public string GetInteractText()
	{
		throw new System.NotImplementedException();
	}
}
