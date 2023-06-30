using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetWhenDead : MonoBehaviour
{ 
    public void ResetScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetActiveScene());
    }
}
