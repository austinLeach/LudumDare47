﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Controls : MonoBehaviour
{
    public AudioSource audio;
    void Start() {
        audio.time = GlobalVariables.timeInAudio;
    }
   public void PlayGame() {
        SceneManager.LoadScene("Level1");
    }
}
