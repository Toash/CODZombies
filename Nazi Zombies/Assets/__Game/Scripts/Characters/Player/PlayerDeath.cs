using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
	public void Death()
	{
		SceneManager.LoadScene(0);
	}
}
