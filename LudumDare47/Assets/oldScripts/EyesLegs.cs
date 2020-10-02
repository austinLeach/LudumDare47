using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesLegs : MonoBehaviour
{
  public Animator animator;
  public Collider2D collider;
  void OnTriggerEnter2D(Collider2D other)
  {
    PlayerMovement controller = other.GetComponent<PlayerMovement>();
    if (controller != null)
    {
      controller.EyesLegs();
      // animator.SetBool("Collected", true);
      Destroy(gameObject);
    }
  }
}
