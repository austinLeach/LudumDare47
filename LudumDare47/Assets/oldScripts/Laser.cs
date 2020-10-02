using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    public ParticleSystem hitEffect;

    void Awake()
    {
        rigidBody2D = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (transform.position.magnitude > 1000f)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(bool shootUp, float force)
    {
        if (shootUp)
        {
            Vector2 direction = new Vector2(0, 1);  //up
            rigidBody2D.AddForce(direction * force);
        }
        else
        {
            Vector2 direction = new Vector2(0, -1); //down
            transform.Rotate(180f, 0f, 0f);
            rigidBody2D.AddForce(direction * force);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement controller = other.GetComponent<PlayerMovement>();
        if (controller != null)
        {
            controller.Died();
        }
        //hitEffect = Instantiate(hitEffect, rigidBody2D.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
