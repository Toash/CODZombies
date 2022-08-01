using UnityEngine;

/// <summary>
/// Spawn point for zombie, needed to spawn zombies, GameManager needs it
/// </summary>
public class ZombieSpawnPoint : MonoBehaviour
{
    public Zone connectedZone; //if zone is activated, zombies can spawn on this

    public bool Active;

    private void Start()
    {
        if (GetZone() == false)
        {
            Debug.LogError(this.name+"does not contain a zone!");
        }
    }

    private void Update()
    {
        if (connectedZone == null) return;
        Active = connectedZone.PlayerInZone;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, .5f);
    }
    private bool GetZone()
    {
        Collider[] cols=Physics.OverlapSphere(transform.position, .1f, Physics.AllLayers, QueryTriggerInteraction.Collide);
        foreach(Collider col in cols)
        {
            Zone zone = col.transform.GetComponent<Zone>();
            if(zone != null)
            {
                connectedZone = zone;
                return true;
            }
        }
        connectedZone = null;
        return false;
    }
}
