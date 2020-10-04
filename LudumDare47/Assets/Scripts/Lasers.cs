﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    public Vector2 ShotFrom;
    bool ShotFromPlayer = false;
    float laserSpeed;


    // Start is called before the first frame update
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(float force, Vector2 direction, bool fromPlayer = false) 
    {
        if (fromPlayer) {
            ShotFromPlayer = true;
        }
        rigidBody2D.AddForce(direction.normalized * force);
        ShotFrom = direction;
        laserSpeed = force;
    }

    public Rigidbody2D GetRigidBody2D()
    {
        return rigidBody2D;
    }
    
    public Vector2 GetShotFrom() {
        return ShotFrom;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Character character = other.GetComponent<Character>();
        if (character) {
            if (ShotFromPlayer) {
                return;
            }
            Debug.Log("Player hit");
            character.TakeDamage();
            Destroy(gameObject);
        }
    }
    public float getLaserSpeed() {
        return laserSpeed;
    }
}
