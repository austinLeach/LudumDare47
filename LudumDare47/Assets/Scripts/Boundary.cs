using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D (Collision2D other) {
        Debug.Log("inside OnCOllision");
        if (other.gameObject.name == "Player") {
            Debug.Log("Inside if");
            other.rigidbody.velocity = Vector3.zero;
        }

    }
}
