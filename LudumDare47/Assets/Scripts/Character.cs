using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera playerCamera;
    public GameObject projectilePreFab;
    public GameObject gun;
    Rigidbody2D rigidBody2D;
     public float mouseSpeed = 10f;

     bool shooting;
     float shootTimer;
     bool damageTaken;
     float damageTimer;
     public float laserSpeed;

     public float Health = 3;

     Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
       rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x = position.x + 10f * horizontal * Time.deltaTime;
        position.y = position.y + 10f * vertical * Time.deltaTime;
        transform.position = position;

        // mouse logic
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, mouseSpeed * Time.deltaTime);

        Timer(ref shooting, ref shootTimer);
        Timer(ref damageTaken, ref damageTimer);

        if (Input.GetButton("Fire1")) {
            Shoot();
        }

        if(Input.GetButtonDown("Dash")) {
            //do this tomorrow ********************************************
        }



        if (Health == 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        lasers.Shoot(laserSpeed, direction, true);  //second number is speed of projectile
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

    public void TakeDamage() {
        // death animation
        if(damageTaken) {
            return;
        }
        damageTaken = true;
        damageTimer = 0.5f;
        Health -= 1;
    }
}
