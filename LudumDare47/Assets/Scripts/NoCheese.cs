using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCheese : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other) {
        Character character = other.GetComponent<Character>();
        if (character) {
            character.TakeDamage();
        }
    }
}
