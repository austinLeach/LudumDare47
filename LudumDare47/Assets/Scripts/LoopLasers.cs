using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopLasers : MonoBehaviour
{

    public GameObject projectilePreFab;
    public float hor;
    public float ver;
    public float laserSpeed;
    
    Rigidbody2D rigidBody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        BossLaser bossLaser = other.GetComponent<BossLaser>();
        if (bossLaser) {
            Destroy(gameObject);
            return;
        }
        
        GameObject lasersObject;
        Lasers lasers; 
        lasersObject = Instantiate(projectilePreFab, other.GetComponent<Lasers>().GetRigidBody2D().position + Vector2.up * ver + Vector2.right * hor, Quaternion.identity);
        lasers = lasersObject.GetComponent<Lasers>();
        lasers.Shoot(laserSpeed, other.GetComponent<Lasers>().GetShotFrom());
        Destroy(other.gameObject);
    }
}
