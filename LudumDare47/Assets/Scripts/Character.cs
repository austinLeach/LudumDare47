using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera playerCamera;
    public GameObject projectilePreFab;
    Rigidbody2D rigidBody2D;

     bool shooting;
     float shootTimer;


    // Start is called before the first frame update
    void Start()
    {
       rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x = position.x + 10f * horizontal * Time.deltaTime;
        position.y = position.y + 10f * vertical * Time.deltaTime;
        transform.position = position;

        Timer(ref shooting, ref shootTimer);

        if (Input.GetButton("Jump")) {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject lasersObject;
        Lasers lasers;
        if (shooting)
        {
            return;
        }
        else
        {
            lasersObject = Instantiate(projectilePreFab, rigidBody2D.position, Quaternion.identity);
        }
        lasers = lasersObject.GetComponent<Lasers>();
        lasers.Shoot(800);  //second number is speed of projectile
        shooting = true;
        shootTimer = 0.1f;
    }

    public bool Timer(ref bool isChanging, ref float timer)
    {
        if (isChanging)
        {
          timer -= Time.deltaTime;
          if (timer < 0)
          {
            isChanging = false;
          }
        }
        return isChanging;
    }
}
