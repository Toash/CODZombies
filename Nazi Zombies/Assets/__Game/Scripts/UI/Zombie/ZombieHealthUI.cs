using UnityEngine;
using TMPro;

public class ZombieHealthUI : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        text.text = $"spawned zombie health: {ServLoc.Inst.ZombieSpawner.zombieSpawnHealth}";

    }
}
