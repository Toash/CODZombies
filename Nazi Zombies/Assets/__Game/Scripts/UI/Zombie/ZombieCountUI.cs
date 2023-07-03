using UnityEngine;
using TMPro;

public class ZombieCountUI : MonoBehaviour
{
    public TMP_Text zombieCountText;

    private void Update()
    {
        int zombieCount = ServLoc.Inst.GameManager.ZombieCount;
        zombieCountText.text = "Zombie count: " + zombieCount.ToString();
    }
}
