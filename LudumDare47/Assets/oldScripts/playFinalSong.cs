using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playFinalSong : MonoBehaviour
{
    public AudioClip final;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player)
        {
            player.GetComponent<AudioSource>().clip = final;
            player.GetComponent<AudioSource>().Play();
            player.killGame();
        }
    }
}
