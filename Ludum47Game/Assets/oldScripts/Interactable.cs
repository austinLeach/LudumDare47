using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SpriteRenderer SpawnSprite;
    public Door door;
    void Start()
    {
        SpawnSprite.GetComponent<SpriteRenderer>();
        SpawnSprite.enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = true;
        door.GetComponent<Door>();
    }

    public void interactedWith()
    {
        SpawnSprite.enabled = true;
        door.Destroy();
    }
}