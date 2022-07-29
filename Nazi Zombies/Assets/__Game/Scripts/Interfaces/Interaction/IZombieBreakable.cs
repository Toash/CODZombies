
/// <summary>
/// Things that can be broken by zombies, such as windows
/// </summary>
public interface IZombieBreakable
{
	public bool Broken { get; set; }
	public void Break();
}
