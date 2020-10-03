using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopLasers : MonoBehaviour
{

    public GameObject projectilePreFab;
    Rigidbody2D rigidBody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        GameObject lasersObject;
        Lasers lasers; 
        lasersObject = Instantiate(projectilePreFab, other.GetComponent<Lasers>().GetRigidBody2D().position + Vector2.up * -20f, Quaternion.identity);
        lasers = lasersObject.GetComponent<Lasers>();
        lasers.Shoot(800);
        Destroy(other.gameObject);
    }
}
