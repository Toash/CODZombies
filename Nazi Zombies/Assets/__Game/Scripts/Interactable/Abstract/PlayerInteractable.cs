using System.Collections;
using UnityEngine;

/// <summary>
/// Can be interacted by player
/// </summary>
public abstract class PlayerInteractable : MonoBehaviour
{
	public enum InteractionType
	{
		OneClick,
		Hold
	}
	/// <summary>
    /// What lets the PlayerInteractable be interacted with. 
    /// </summary>
	public enum DetectionType
	{
		Raycast,
		Collider
	}

	/// <summary>
    /// Time before another interaction can occur on this object
    /// </summary>
    [field: SerializeField]
	public float Cooldown { get; private set; }
	[field: SerializeField]
	public InteractionType InteractType { get; private set; }
	[field: SerializeField]
	public DetectionType DetectType { get; private set; }
	[field:SerializeField]
	public string InteractString { get; private set; }

	private float timer = 0;

	private bool ready { get { return timer >= this.Cooldown; } }

	public virtual void Interact()
    {
		if (!ready) return;
		ResetTimers();
    }

    private void Update()
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
