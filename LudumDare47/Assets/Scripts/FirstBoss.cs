using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Beam;
    public Character character;
    public healthBar healthBar;
    public Animator animator;
    Rigidbody2D rigidBody2D;

    Transform target;
    public float speed = 15f;

    bool shooting = true;
    float shootingTimer = 2f;
    bool shootingDownTime = false;
    float shootingDownTimer;
    bool beaming = false;
    float beamTimer = 3f;

    bool damageTaken = false;
    float damageTimer;

    public float Health = 500;
    public AudioSource audio;
    public AudioClip BeamSound;
    void Start()
    {
        character.GetComponent<Character>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        Beam.GetComponent<Renderer>().enabled = false;
        Beam.GetComponent<BoxCollider2D>().enabled = false;
        healthBar.setMaxHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
        character.Timer(ref shooting, ref shootingTimer);
        character.Timer(ref shootingDownTime, ref shootingDownTimer);
        character.Timer(ref damageTaken, ref damageTimer);
        character.Timer(ref beaming, ref beamTimer);

        if (shooting == false && shootingDownTime == false) {
            animator.SetBool("shooting", true);
            shooting = true;
            shootingTimer = 1.4f;
            shootingDownTime = true;
            shootingDownTimer = 3.5f;
        }
        if (shooting == true) {
            
            Beam.GetComponent<Renderer>().enabled = true;

            if (shootingTimer < 1f) {
                Beam.GetComponent<BoxCollider2D>().enabled = true;
                PlayBeamSound();
            }  
        }

        if (shooting == false) {
            Beam.GetComponent<Renderer>().enabled = false;
            Beam.GetComponent<BoxCollider2D>().enabled = false;
            animator.SetBool("shooting", false);
            target = character.transform;
            transform.position = Vector2.MoveTowards (transform.position, new Vector2(target.position.x, transform.position.y), speed*Time.deltaTime);
        }
        if (Health < 50 ) {
            speed = 25f;
        }
        if (Health < 0) {
            Destroy(gameObject);
            SceneManager.LoadScene("InbetweenLevel1");
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
        Health -= 1;
        healthBar.SetHealth(Health);
    }
    void PlayBeamSound() {
        if (beaming) {
            return;
        }
        beaming = true;
        beamTimer = 3f;
        audio.PlayOneShot(BeamSound);
    }
    public bool inStartUp() {

        return shooting;
    }
}
