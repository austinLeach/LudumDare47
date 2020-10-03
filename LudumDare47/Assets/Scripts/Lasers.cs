using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    Rigidbody2D rigidBody2D;


    // Start is called before the first frame update
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(float force)
    {
        Vector2 direction = new Vector2(0, 1);
        rigidBody2D.AddForce(direction * force);
    }

    public Rigidbody2D GetRigidBody2D()
    {
        return rigidBody2D;
    }
}
