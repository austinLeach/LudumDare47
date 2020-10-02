using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBox : MonoBehaviour
{
    public GameObject projectilePreFab;
    Rigidbody2D rigidBody2D;
    bool isShooting = false;
    public float shootingTimer = 2f;
    public float delayStart = 0f;
    float timer;
    public bool shootUp;
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        timer = shootingTimer;
    }
    void Update()
    {
        delayStart -= Time.deltaTime;
        if (delayStart > 0)
        {
            return;
        }
        isShooting = Timer(ref isShooting, ref shootingTimer);
        Shoot();
    }
    void Shoot()
    {
        GameObject projectileObject;
        Laser projectile;
        if (isShooting)
        {
            return;
        }
        if (shootUp)
        {
            projectileObject = Instantiate(projectilePreFab, rigidBody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        }
        else
        {
            projectileObject = Instantiate(projectilePreFab, rigidBody2D.position + Vector2.up * -0.5f, Quaternion.identity);
        }
        projectile = projectileObject.GetComponent<Laser>();
        projectile.Shoot(shootUp, 400);  //second number is speed of projectile
        isShooting = true;
        shootingTimer = timer;
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
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
