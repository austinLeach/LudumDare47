using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class YouWin : MonoBehaviour
{
    public AudioSource audio;
    void Start() {
        audio.time = 0f;
    }
    public void MainMenu() {
        GlobalVariables.timeInAudio = audio.time;
        SceneManager.LoadScene("MainMenu");
    }
}
