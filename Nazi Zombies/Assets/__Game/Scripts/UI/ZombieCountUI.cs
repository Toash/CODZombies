using UnityEngine;
using TMPro;

public class ZombieCountUI : MonoBehaviour
{
    public TMP_Text zombieCountText;

    private void Update()
    {
        int zombieCount = ServiceLocator.Instance.GameManager.CurrentZombies;
        zombieCountText.text = "Zombie count: " + zombieCount.ToString();
    }
}
