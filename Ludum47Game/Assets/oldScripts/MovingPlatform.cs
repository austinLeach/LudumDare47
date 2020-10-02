using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    float moveSpeed = 3f;
    bool moveRight = true;
    bool changeDir = false;
    public float moveTime = 2f;
    float changeTimer;
    public bool up = false;

    void Start()
    {
        if (!up)
        {
            changeTimer = moveTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            if (moveRight == true)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
            changeTimer = moveTime;
        }
        if (!up)
        {
            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
        }
        else
        {
            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
            }
        }
    }
}
