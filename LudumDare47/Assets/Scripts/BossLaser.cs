﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    public FirstBoss firstBoss;
    public FinalBoss finalBoss;

    public bool inStartUp;
    // Start is called before the first frame update
    void Start()
    {
        firstBoss = firstBoss.GetComponent<FirstBoss>();
        finalBoss = finalBoss.GetComponent<FinalBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        inStartUp = firstBoss.inStartUp();
    }

    void OnTriggerStay2D(Collider2D other) {
        Character character = other.GetComponent<Character>();
        Lasers lasers = other.GetComponent<Lasers>();
        if (character) {
            character.TakeDamage();
        }
    }
}
