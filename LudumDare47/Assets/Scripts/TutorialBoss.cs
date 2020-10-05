using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audio;

    public healthBar healthBar;
    public float Health = 10;
    void Start()
    {
        audio.time = GlobalVariables.timeInAudio;
        healthBar.setMaxHealth(Health);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0) {
            Destroy(gameObject);
            GlobalVariables.timeInAudio = audio.time;
            SceneManager.LoadScene("InbetweenTutorial");
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Here");
        Lasers lasers = other.GetComponent<Lasers>();
        if (lasers) {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage() {
        Health -= 1;
        healthBar.SetHealth(Health);
    }
}
