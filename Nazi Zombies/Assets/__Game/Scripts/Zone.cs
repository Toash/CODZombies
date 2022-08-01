using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Zones activate certain spawn points
/// </summary>
[TypeInfoBox("When the player steps in this zone, the corresponding Zombie Spawn Point will activate.")]
[RequireComponent(typeof(BoxCollider))]
public class Zone : MonoBehaviour
{
    public bool PlayerInZone { get; private set; }

    private bool isPlayer(Collider other)
    {
        return other.transform.GetComponent<PlayerRef>();
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer(other))
        {
            PlayerInZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isPlayer(other))
        {
            PlayerInZone = false;
        }
    }

}
