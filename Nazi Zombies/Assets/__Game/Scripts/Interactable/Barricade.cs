using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Barricade : PlayerInteractable, IZombieBreakable
{
    [SerializeField]
    private BarricadeStats stats;

	[SerializeField]
	private AudioSource RepairSound;



    public int CurrentWood { get; private set; }
    public bool Broken { get; set; }

	public UnityEvent AtLeastOneBarricade;
    public UnityEvent NoBarricades;


    private void Awake()
    {
        CurrentWood = stats.MaxWood;
    }
	private void Update()
	{
		if (CurrentWood > 0)
		{
            Broken = false;
		}
	}

	public override string GetInteractString()
	{
        return base.InteractString;
	}

	public override void Interact()
	{
		base.Interact();
		Repair();
		PlayRepairSound();
		
	}

	public void Break()
	{
		CurrentWood -= 1;
		if (CurrentWood <= 0)
		{
			Broken = true;
			NoBarricades?.Invoke();
		}
	}
	private void Repair()
    {
		CurrentWood += 1;
    }
	private void PlayRepairSound()
    {
		if (this.RepairSound == null) return;
		this.RepairSound.Play();
    }
}
