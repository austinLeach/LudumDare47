using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    public Vector2 ShotFrom;


    // Start is called before the first frame update
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(float force, Vector2 direction) 
    {
        rigidBody2D.AddForce(direction * force);
        ShotFrom = direction;
    }

    public Rigidbody2D GetRigidBody2D()
    {
        return rigidBody2D;
    }
    
    public Vector2 GetShotFrom() {
        return ShotFrom;
    }
}
