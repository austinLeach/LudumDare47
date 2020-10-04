using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioInbetweenLevel2 : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        audio.time = 52.7f;
    }
    void Update() {
        Debug.Log(audio.time);
    }
}
