using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Spawn point for zombie
/// </summary>
public class ZombieSpawnPoint : MonoBehaviour
{
    //if zone is activated, zombies can spawn on this
    public Zone[] connectedZones { get; private set; }

    public bool Active;

    private void Start()
    {
        connectedZones = GetZones();
        if (connectedZones.Length == 0)
        {
            Debug.LogError(this.name + ": does not contain a zone!");
        }
    }

    private void Update()
    {
        UpdateSpawnPointState();
    }

    private void UpdateSpawnPointState()
    {
        foreach (Zone zone in connectedZones)
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
