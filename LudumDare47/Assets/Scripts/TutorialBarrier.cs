using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBarrier : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        Lasers lasers = other.GetComponent<Lasers>();
        if (lasers) {
            Destroy(other.gameObject);
        }
    }
}
