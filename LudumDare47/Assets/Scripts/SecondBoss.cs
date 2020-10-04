using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondBoss : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    Transform target;

    public Character character;
    public GameObject projectilePreFab;
    public GameObject firePoint;
    public healthBar healthBar;

    bool shooting;
     float shootTimer;

     bool damageTaken = false;
    float damageTimer;

    public float laserSpeed;

    public float Health = 300;

    Vector2 direction;

    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        character.GetComponent<Character>();
        healthBar.setMaxHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
        target = character.transform;
        Vector3 fds = new Vector3(0,0,1);

        firePoint.transform.LookAt(target, fds);

        Shoot();

        character.Timer(ref shooting, ref shootTimer);

        if (Health < 0) {
            Destroy(gameObject);
            SceneManager.LoadScene("Level3");
        }
    }

    void Shoot()
    {
        direction = (target.position - firePoint.transform.position);
        GameObject lasersObject;
        Lasers lasers;
        if (shooting)
        {
            return;
        }
        else
        {
            lasersObject = Instantiate(projectilePreFab, firePoint.transform.position, Quaternion.identity);
        }
        lasers = lasersObject.GetComponent<Lasers>();
        lasers.Shoot(laserSpeed, direction, false, true);  //second number is speed of projectile
        shooting = true;
        shootTimer = 0.4f;
        if (Health < 150) {
            shootTimer = 0.2f;
            laserSpeed = 1000f;
        }
    }
    void OnTriggerStay2D(Collider2D other) {
        Character character = other.GetComponent<Character>();
        Lasers lasers = other.GetComponent<Lasers>();

        if (character) {
            character.TakeDamage();
        }
        if (lasers) {
            if (other.GetComponent<Lasers>().Boss) {
                return;
            }
            TakeDamage();
            Destroy(other.gameObject);
        }
    }
    public void TakeDamage() {
        Health -= 1;
        healthBar.SetHealth(Health);
    }
}
