using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioInbetweenLevel1 : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        audio.time = 30f;
    }
    void FixedUpdate() {
        Debug.Log(audio.time);
    }
}
