using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int numberToProceed = 6;
    PlayerMovement player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.getTargetsHit() == numberToProceed)
        {
            Destroy();
            player.resetTargetsHit();
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
