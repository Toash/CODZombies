using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

/// <summary>
/// Spawn point for zombie
/// </summary>
public class ZombieSpawnPoint : MonoBehaviour
{
    [field: SerializeField, InfoBox("What zones activate this spawnpoint")]
    public Zone[] ConnectedZones { get; private set; }

    [ShowInInspector,ReadOnly]
    public bool Active { get; private set; }

    private void Update()
    {
        UpdateSpawnPointState();
    }

    private void UpdateSpawnPointState()
    {
        foreach (Zone zone in ConnectedZones)
        {
            if (zone.PlayerInZone == true)
            {
                Active = true;
                return;
            }
        }
        Active = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, .5f);
    }
    private Zone[] GetZones()
    {
        List<Zone> zones = new List<Zone>();
        Collider[] cols=Physics.OverlapSphere(transform.position, .1f, Physics.AllLayers, QueryTriggerInteraction.Collide);
        foreach(Collider col in cols)
        {
            Zone zone = col.transform.GetComponent<Zone>();
            if(zone != null)
            {
                zones.Add(zone);

            }
        }
        return zones.ToArray();
    }
}
