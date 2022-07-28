
/// <summary>
/// This interface is used for one time events 
/// </summary>
public interface IPlayerInteractable
{
	public string InteractText { get; set; }
	public void PlayerInteract();
}
