using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

  public CharacterController2D controller;
  public Animator animator;
  public Sprite eyesLegsSprite;
  [SerializeField] private GameObject gunSprite;
  [SerializeField] private GameObject wingsSprite;
  [SerializeField] private GameObject crownSprite;
  public Cinemachine.CinemachineVirtualCamera playerCamera;
  public Cinemachine.CinemachineVirtualCamera finalCamera;

  AudioSource audio;
  public AudioClip loop1;
  public AudioClip loop2;
  public AudioClip loop3;
  public AudioClip loop4;
  public AudioClip loop5;
  public AudioClip jingle;
  public AudioClip gunSound;
  Rigidbody2D rigidBody2D;

  public float runSpeed = 40f;

  float horizontalMove = 0f;
    float verticalMove = 0f;
  bool abilityToJump = false;
  bool hasGun = false;
  bool abilityToFly = false;
  bool jump = false;
  bool flying = false;
  bool dead = false;

  int numberOfTargets = 2;
  int targetsHit = 0;
  bool hitTarget = false;
  float targetHitTimer;

  bool shooting;
  float shootTimer;
  bool jingling;
  float jingleTimer;

  bool killing;
  float killTime;

  int pedistalPlaced = 0;

  public GameObject projectilePreFab;

  // Start is called before the first frame update
  void Start()
  {
    audio = GetComponent<AudioSource>();
    audio.clip = loop1;
    audio.Play();

    gunSprite.GetComponent<SpriteRenderer>().enabled = false;
    wingsSprite.GetComponent<SpriteRenderer>().enabled = false;
    crownSprite.GetComponent<SpriteRenderer>().enabled = false;

    controller = this.GetComponent<CharacterController2D>();
  }

  // Update is called once per frame
  void Update()
  {
    rigidBody2D = controller.GetRigidBody2D();
    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
    verticalMove = controller.getVelocityY();

    if (controller.disabled)
    {
        animator.SetFloat("Speed", 0f);
    }

    if (!controller.disabled)
    {
      animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
      animator.SetFloat("verticalSpeed", verticalMove);
      wingsSprite.GetComponent<Animator>().SetFloat("verticalSpeed", verticalMove);
    }

    shooting = Timer(ref shooting, ref shootTimer);

    if (Input.GetButtonDown("Jump") && abilityToJump == true)
    {
      jump = true;
    }
    if (Input.GetButton("Jump") && abilityToFly == true)
    {
        flying = true;
    } else
    {
        flying = false;
    }

    if (dead)
    { 
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    if (Input.GetButtonDown("Fire1") && hasGun == true)
    {
      Shoot();
    }

    if (Input.GetKeyDown(KeyCode.X))
    {
        RaycastHit2D hit = Physics2D.Raycast(rigidBody2D.position + Vector2.up * 0.2f, controller.getDirection(), 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            Interactable podium = hit.collider.GetComponent<Interactable>();
            if (podium != null)
            {
                podium.interactedWith();
                pedistalPlaced++;
                DestroyItem();
            }
        }
    }

    hitTarget = Timer(ref hitTarget, ref targetHitTimer);
    killing = killTimer(ref killing, ref killTime);
  }

  public void OnLanding()
  {
    animator.SetBool("isJumping", false);
  }

  void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "Ground")
    {
      animator.SetBool("isJumping", true);
    }
    MovingPlatform platform = other.gameObject.GetComponent<MovingPlatform>();
    if (platform != null)
    {
        this.transform.parent = null;
    }
 }

  void OnCollisionStay2D(Collision2D other)
  {
    if (other.gameObject.tag == "Ground")
    {
      animator.SetBool("isJumping", false);
    }
    MovingPlatform platform = other.gameObject.GetComponent<MovingPlatform>();
    if (platform != null)
    {
        this.transform.parent = other.transform;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
        Target target = other.gameObject.GetComponent<Target>();
        if (target)
        {
            TargetCount();
        }
  }

  void FixedUpdate()
  {
    controller.Move(horizontalMove * Time.deltaTime, false, jump, flying, abilityToFly);
    jump = false;
  }

  void LateUpdate()
  {

  }

  public void EyesLegs()
  {
    this.GetComponent<SpriteRenderer>().sprite = eyesLegsSprite;
    abilityToJump = true;
    playerCamera.Priority = 20;
    audio.PlayOneShot(jingle, 0.5f);
    audio.clip = loop2;
    audio.Play();
    controller.disabled = true;
    controller.animationTimer = 2.0f;
    animator.SetTrigger("eyeAnimation");
  }

    public void GunCollected()
    {
      gunSprite.GetComponent<SpriteRenderer>().enabled = true;
      hasGun = true;
      audio.PlayOneShot(jingle, 0.5f);
      audio.clip = loop3;
      audio.Play();
    }

  void Shoot()
  {
    GameObject projectileObject;
    Projectile projectile;
    if (shooting)
    {
      return;
    }
    if (controller.m_FacingRight)
    {
      projectileObject = Instantiate(projectilePreFab, rigidBody2D.position + Vector2.up * 0.05f + Vector2.right * 1.5f, Quaternion.identity);
    }
    else
    {
      projectileObject = Instantiate(projectilePreFab, rigidBody2D.position + Vector2.up * 0.05f + Vector2.right * -1.5f, Quaternion.identity);
    }
    projectile = projectileObject.GetComponent<Projectile>();
    projectile.Shoot(controller.m_FacingRight, 800);  //second number is speed of projectile
    audio.PlayOneShot(gunSound);
    shooting = true;
    shootTimer = 0.25f;

    animator.SetTrigger("Shooting");
  }
    public void TargetCount()
    {
        if(hitTarget)
        {
            return;
        }
        targetsHit++;
        targetHitTimer = 0.1f;
        hitTarget = true;
    }

    public void CanFly()
    {
        wingsSprite.GetComponent<SpriteRenderer>().enabled = true;
        abilityToFly = true;
        abilityToJump = false;
        audio.PlayOneShot(jingle, 0.5f);
        audio.clip = loop4;
        audio.Play();
    }
    public void Crown()
    {
        crownSprite.GetComponent<SpriteRenderer>().enabled = true;
        finalCamera.Priority = 30;
        audio.PlayOneShot(jingle, 0.5f);
        audio.clip = loop5;
        audio.Play();
    }

    public void Died()
    {
        dead = true;
        Debug.Log("died");
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
    public bool killTimer(ref bool isChanging, ref float timer)
    {
        if (isChanging)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                isChanging = false;
                Died();
            }
        }
        return isChanging;
    }
    public int getTargetsHit()
    {
        return targetsHit;
    }
    public void resetTargetsHit()
    {
        targetsHit = 0;
    }
    void DestroyItem()
    {
        if (pedistalPlaced == 1)
        {
            crownSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (pedistalPlaced == 2)
        {
            wingsSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (pedistalPlaced == 3)
        {
            gunSprite.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (pedistalPlaced == 4)
        {
            animator.SetTrigger("removeEye");
        }
        return;
    }
    public void killGame()
    {
        animator.SetTrigger("final");
        killing = true;
        killTime = 11f;
    }
}
