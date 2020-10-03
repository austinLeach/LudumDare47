﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    public float velocityX;
    public float velocityY;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.x = position.x + velocityX * Time.deltaTime;
        position.y = position.y + velocityY * Time.deltaTime;
        transform.position = position;
    }
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("inside on trigger");
        Lasers lasers = other.GetComponent<Lasers>();
        Character character = other.GetComponent<Character>();
        
        if (lasers != null) {
            Debug.Log("destroying");
            Destroy(other.gameObject);
        }
        if (character != null) {
            Debug.Log("Character hit");
            
        }
    }
}