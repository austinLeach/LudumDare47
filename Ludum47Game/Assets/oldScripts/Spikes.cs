using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public bool up;
    public bool down;
    public bool rightleft;
    public Sprite spikeUp;
    public Sprite spikeDown;
    public Sprite spikeRightLeft;

    void Start()
    {
        if (up)
        {
            this.GetComponent<SpriteRenderer>().sprite = spikeUp;
        }
        if (down)
        {
            this.GetComponent<SpriteRenderer>().sprite = spikeDown;
        }
        if (rightleft)
        {
            this.GetComponent<SpriteRenderer>().sprite = spikeRightLeft;
        }
    }


  void OnTriggerStay2D(Collider2D other)
  {
    PlayerMovement controller = other.GetComponent<PlayerMovement>();
    if(controller != null) {
         controller.Died();
    }
  }
}
