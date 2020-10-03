﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Beam;
    public Character character;
    Rigidbody2D rigidBody2D;

    Transform target;
    public float speed = 1f;

    bool shooting = false;
    float shootingTimer;
    bool shootingDownTime = false;
    float shootingDownTimer;

    bool damageTaken = false;
    float damageTimer;

    public float Health = 500;
    void Start()
    {
        character.GetComponent<Character>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        Beam.GetComponent<Renderer>().enabled = false;
        Beam.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        character.Timer(ref shooting, ref shootingTimer);
        character.Timer(ref shootingDownTime, ref shootingDownTimer);
        character.Timer(ref damageTaken, ref damageTimer);

        if (shooting == false && shootingDownTime == false) {
            shooting = true;
            shootingTimer = 1f;
            shootingDownTime = true;
            shootingDownTimer = 3f;
        }
        if (shooting == true) {
            Beam.GetComponent<Renderer>().enabled = true;
            Beam.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (shooting == false) {
            Beam.GetComponent<Renderer>().enabled = false;
            Beam.GetComponent<BoxCollider2D>().enabled = false;

            target = character.transform;
            transform.position = Vector2.MoveTowards (transform.position, new Vector2(target.position.x, transform.position.y), 15*Time.deltaTime);
        }
        if (Health < 0) {
            Destroy(gameObject);
        }

        //Debug.Log(Health);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Character character = other.GetComponent<Character>();
        Lasers lasers = other.GetComponent<Lasers>();
        if (character) {
            character.TakeDamage();
        }
        if (lasers) {
            TakeDamage();
            Destroy(other.gameObject);
        }

    }

    public void TakeDamage() {
        if(damageTaken) {
            return;
        }
        damageTaken = true;
        damageTimer = 0.02f;
        Health -= 1;
    }
}
