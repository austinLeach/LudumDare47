using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Collider2D collider;
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
