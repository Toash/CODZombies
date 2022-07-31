using UnityEngine;

/// <summary>
/// Spawn point for zombie, needed to spawn zombies, GameManager needs it
/// </summary>
public class ZombieSpawnPoint : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, .5f);
    }
}
