using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera playerCamera;
    public GameObject projectilePreFab;
    public GameObject gun;
    Rigidbody2D rigidBody2D;
     public float mouseSpeed = 10f;

     bool shooting;
     float shootTimer;

     Vector2 direction;


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

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, mouseSpeed * Time.deltaTime);

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
        lasers.Shoot(400, direction);  //second number is speed of projectile
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
