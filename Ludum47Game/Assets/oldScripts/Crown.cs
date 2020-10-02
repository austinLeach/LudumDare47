using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    public Animator animator;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement controller = other.GetComponent<PlayerMovement>();
        if (controller != null)
        {
            controller.Crown();
            // animator.SetBool("Collected", true);
            Destroy(gameObject);
        }
    }
}
