using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Dead : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        audio.time = 100f;
    }
    public void TryAgain() {
        SceneManager.LoadScene(GlobalVariables.previousScene);
    }
    public void EasyMode() {
        GlobalVariables.EasyMode = true;
        GlobalVariables.Health = 50f;
        SceneManager.LoadScene(GlobalVariables.previousScene);
    }
}
