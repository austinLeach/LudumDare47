using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InbetweenLevel1 : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene("Level2");
    }
}
