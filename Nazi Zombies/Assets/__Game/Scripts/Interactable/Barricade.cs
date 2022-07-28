using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

// should barricades be the same as interactables?
//
//- You dont use raycast to select them, only triggers.
//- The player can hold down e to repair, other interactables are only one time events.
//- Barricades differ specifically in selecting them and "interacting" with them.
//
public class Barricade : Interactable, IDamagable
{
    [SerializeField]
    private BarricadeStats stats;
    public int CurrentWood { get; private set; }

    public UnityEvent AtLeastOneBarricade;
    public UnityEvent NoBarricades;

    private bool noMoreWood;

    private void Awake()
    {
        CurrentWood = stats.MaxWood;
    }
    public void Damage(int amount)
    {
        CurrentWood -= 1;
        if (CurrentWood <= 0) NoBarricades?.Invoke();
    }

	public override string GetInteractString()
	{
        return "repair";
	}

	public override void Interact()
	{
        Debug.Log("Repairing");
        CurrentWood += 1;
	}
}
