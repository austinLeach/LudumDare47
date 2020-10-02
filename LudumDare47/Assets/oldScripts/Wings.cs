using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MonoBehaviour
{
    public Animator animator;
    public Collider2D collider;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement controller = other.GetComponent<PlayerMovement>();
        if (controller != null)
        {
            controller.CanFly();
            // animator.SetBool("Collected", true);
            Destroy(gameObject);
        }
    }
}
