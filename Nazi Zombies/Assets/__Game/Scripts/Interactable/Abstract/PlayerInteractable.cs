using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Defines what can be interacted by player
/// </summary>
public abstract class PlayerInteractable : MonoBehaviour
{

	public enum InteractionType
	{
		OneClick,
		Hold
	}
	public enum DetectionType
	{
		Raycast,
		Collider
	}

    [field: SerializeField]
	public float Cooldown { get; private set; }
	[field: SerializeField]
	public InteractionType InteractType { get; private set; }
	[field: SerializeField]
	public DetectionType DetectType { get; private set; }
	[field:SerializeField]
	public string InteractString { get; private set; }

	[ShowInInspector,ReadOnly,PropertyOrder(-1)]
	private float timer = 0;

	private bool ready { get { return timer >= this.Cooldown; } }

	public virtual bool Interact()
    {
		if (!ready) return false;
		ResetTimers();
		return true;
    }

    public virtual void Update()
    {
		UpdateTimers();
    }
	private void UpdateTimers()
    {
		this.timer += Time.deltaTime;
    }
	private void ResetTimers()
    {
		this.timer = 0;
    }
}
