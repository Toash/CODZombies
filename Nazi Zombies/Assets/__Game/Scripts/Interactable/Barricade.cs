using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Barricade : PlayerInteractable, IZombieBreakable
{
    public int CurrentWood { get; private set; }
    public bool Broken { get; set; }

    [SerializeField]
	private int maxWood = 7;
	[SerializeField]
	private AudioSource repairSound;



	public UnityEvent AtLeastOneBarricade;
    public UnityEvent NoBarricades;

	private bool HasWood() { return CurrentWood>0; }
	private bool UnderMaxWood() { return CurrentWood <= maxWood; }


    private void Awake()
    {
        CurrentWood = maxWood;
    }
	public override void Update()
	{
		base.Update();
		if (HasWood())
		{
            Broken = false;
		}
	}

	public override bool Interact()
	{
		if(base.Interact() && UnderMaxWood()){
            Repair();
			return true;
        }
		return false;

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
		IncreaseWood();
		PlayRepairSound();
    }
	private void IncreaseWood()
    {
		CurrentWood += 1;
    }
	private void PlayRepairSound()
    {
		if (this.repairSound == null) return;
		this.repairSound.Play();
    }
}
