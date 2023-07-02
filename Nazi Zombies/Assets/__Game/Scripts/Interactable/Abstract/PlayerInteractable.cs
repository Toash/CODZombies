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

	[field: SerializeField, Tooltip("In seconds")]
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


	protected bool CanInteract(){
		return ready;
	}

	protected void ResetTimer(){
		this.timer = 0;
	}

	/// <summary>
	/// Cooldown built in
	/// </summary>
	public virtual void Interact(){
	    if (!ready) return; //End the interaction early
		ResetTimer();
	}

    public virtual void Update()
    {
		UpdateTimers();
    }
	private void UpdateTimers()
    {
		this.timer += Time.deltaTime;
    }
}
