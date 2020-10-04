using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalBoss : MonoBehaviour
{
    public Character character;
    public GameObject Beam1;
    public GameObject Beam2;
    public GameObject Beam3;
    public GameObject Beam4;
    public GameObject Beam5;

    public GameObject firePoint;
    public GameObject projectilePreFab;
    public float laserSpeed;

    public healthBar healthBar;
    Rigidbody2D rigidBody2D;

    bool shootingLaser = false;
    float shootingLaserTimer;

    bool shootingBeam = false;
    float shootingBeamTimer = 4f;
    bool shootingDownTime = true;
    float shootingDownTimer = 4f;
    bool beaming = false;
    float beamTimer = 3f;

    public float Health = 500;
    public AudioSource audio;
    public AudioClip BeamSound;
    public AudioClip LaserSound;
    Vector2 direction;
    Transform target;

    
    // Start is called before the first frame update
    void Start()
    {
        character.GetComponent<Character>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        Beam1.GetComponent<Renderer>().enabled = false;
        Beam1.GetComponent<BoxCollider2D>().enabled = false;
        Beam2.GetComponent<Renderer>().enabled = false;
        Beam2.GetComponent<BoxCollider2D>().enabled = false;
        Beam3.GetComponent<Renderer>().enabled = false;
        Beam3.GetComponent<BoxCollider2D>().enabled = false;
        Beam4.GetComponent<Renderer>().enabled = false;
        Beam4.GetComponent<BoxCollider2D>().enabled = false;
        Beam5.GetComponent<Renderer>().enabled = false;
        Beam5.GetComponent<BoxCollider2D>().enabled = false;
        healthBar.setMaxHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
        character.Timer(ref shootingBeam, ref shootingBeamTimer);
        character.Timer(ref shootingLaser, ref shootingLaserTimer);
        character.Timer(ref shootingDownTime, ref shootingDownTimer);
        character.Timer(ref beaming, ref beamTimer);

        target = character.transform;
        Vector3 fds = new Vector3(0,0,1);
        firePoint.transform.LookAt(target, fds);


        if (shootingBeam == false && shootingDownTime == false) {
            shootingBeam = true;
            shootingBeamTimer = 4f;
            shootingDownTime = true;
            shootingDownTimer = 12f;
            PlayBeamSound();
        }
        if (shootingBeam == true) {
            Beam1.GetComponent<Renderer>().enabled = true;
            Beam1.GetComponent<BoxCollider2D>().enabled = true;
            Beam2.GetComponent<Renderer>().enabled = true;
            Beam2.GetComponent<BoxCollider2D>().enabled = true;
            Beam3.GetComponent<Renderer>().enabled = true;
            Beam3.GetComponent<BoxCollider2D>().enabled = true;
            Beam4.GetComponent<Renderer>().enabled = true;
            Beam4.GetComponent<BoxCollider2D>().enabled = true;
            Beam5.GetComponent<Renderer>().enabled = true;
            Beam5.GetComponent<BoxCollider2D>().enabled = true;
            
        }
        if (shootingBeam == false) {
            Beam1.GetComponent<Renderer>().enabled = false;
            Beam1.GetComponent<BoxCollider2D>().enabled = false;
            Beam2.GetComponent<Renderer>().enabled = false;
            Beam2.GetComponent<BoxCollider2D>().enabled = false;
            Beam3.GetComponent<Renderer>().enabled = false;
            Beam3.GetComponent<BoxCollider2D>().enabled = false;
            Beam4.GetComponent<Renderer>().enabled = false;
            Beam4.GetComponent<BoxCollider2D>().enabled = false;
            Beam5.GetComponent<Renderer>().enabled = false;
            Beam5.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (Health < 100) {
            Shoot();
        }
        else if (shootingDownTimer < 7 && shootingDownTimer > 1) {
            Shoot();
        }
        if (Health < 50) {
            Shoot();
        }
        if (Health < 0) {
            Destroy(gameObject);
            SceneManager.LoadScene("YouWin");
        }
    }
    void OnTriggerStay2D(Collider2D other) {
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
        Health -= 1;
        healthBar.SetHealth(Health);
    }
    void Shoot()
    {
        direction = (target.position - firePoint.transform.position);
        GameObject lasersObject;
        Lasers lasers;
        if (shootingLaser)
        {
            return;
        }
        else
        {
            lasersObject = Instantiate(projectilePreFab, firePoint.transform.position, Quaternion.identity);
        }
        lasers = lasersObject.GetComponent<Lasers>();
        lasers.Shoot(laserSpeed, direction, false, true);  //second number is speed of projectile
        shootingLaser = true;
        shootingLaserTimer = 0.4f;
        audio.PlayOneShot(LaserSound);
        if (Health < 200) {
            shootingLaserTimer = 0.2f;
            laserSpeed = 1000f;
        }
    }
    void PlayBeamSound() {
        if (beaming) {
            return;
        }
        beaming = true;
        beamTimer = 8f;
        audio.PlayOneShot(BeamSound);
    }
}
