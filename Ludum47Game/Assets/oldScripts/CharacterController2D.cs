using UnityEngine;
using UnityEngine.Events;


//Modified basic character controller that I reused from a previous tutorial/project
public class CharacterController2D : MonoBehaviour
{
  [SerializeField] private float m_JumpForce = 400f;              // Amount of force added when the player jumps.
    public float flySpeed = 100f;
    public float gravityScale = 0.7f;
  [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;      // Amount of maxSpeed applied to crouching movement. 1 = 100%
  [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
  [SerializeField] private bool m_AirControl = false;             // Whether or not a player can steer while jumping;
  [SerializeField] private LayerMask m_WhatIsGround;              // A mask determining what is ground to the character
  [SerializeField] private Transform m_GroundCheck;             // A position marking where to check if the player is grounded.
  [SerializeField] private Transform m_GroundCheck2;             // A position marking where to check if the player is grounded.
  [SerializeField] private Transform m_CeilingCheck;              // A position marking where to check for ceilings
  [SerializeField] private Collider2D m_CrouchDisableCollider;        // A collider that will be disabled when crouching

  const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
  private bool m_Grounded;            // Whether or not the player is grounded.
  const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
  private Rigidbody2D m_Rigidbody2D;
  public bool m_FacingRight = true;  // For determining which way the player is currently facing.
  private Vector3 m_Velocity = Vector3.zero;

  [Header("Events")]
  [Space]

  public UnityEvent OnLandEvent;

  [System.Serializable]
  public class BoolEvent : UnityEvent<bool> { }

  public float FallSpeed = 100f;
    public bool disabled = false;
    public float animationTimer;

  private void Awake()
  {
    m_Rigidbody2D = GetComponent<Rigidbody2D>();

    if (OnLandEvent == null)
      OnLandEvent = new UnityEvent();
  }

  private void FixedUpdate()
  {
    bool wasGrounded = m_Grounded;
    m_Grounded = false;

    // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
    // This can be done using layers instead but Sample Assets will not overwrite your project settings.
    Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
    for (int i = 0; i < colliders.Length; i++)
    {
      if (colliders[i].gameObject != gameObject)
      {
        m_Grounded = true;
        if (!wasGrounded)
          OnLandEvent.Invoke();
      }
    }
    Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_GroundCheck2.position, k_GroundedRadius, m_WhatIsGround);
    for (int i = 0; i < colliders2.Length; i++)
    {
        if (colliders2[i].gameObject != gameObject)
        {
            m_Grounded = true;
            if (!wasGrounded)
                OnLandEvent.Invoke();
        }
    }
    }

  public void Move(float move, bool crouch, bool jump, bool flying, bool hasFlying)
  {
    disabled = Timer(ref disabled, ref animationTimer);
    if(disabled)
    {
        m_Rigidbody2D.velocity = new Vector2(0f, 0f);
        return;
    }

    //only control the player if grounded or airControl is turned on
    if (m_Grounded || m_AirControl)
    { 
      // Move the character by finding the target velocity
      Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
      // And then smoothing it out and applying it to the character
      m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

      // If the input is moving the player right and the player is facing left...
      if (move > 0 && !m_FacingRight)
      {
        // ... flip the player.
        Flip();
      }
      // Otherwise if the input is moving the player left and the player is facing right...
      else if (move < 0 && m_FacingRight)
      {
        // ... flip the player.
        Flip();
      }
    }
    // If the player should jump...
    if (m_Grounded && jump)
    {
      // Add a vertical force to the player.
      m_Grounded = false;
      m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
    }
    if (m_Rigidbody2D.velocity.y < 0 && !hasFlying) //make the fallspeed with wings bareable
    {
      m_Rigidbody2D.AddForce(new Vector2(0f, -FallSpeed));
    }
    if (flying) 
    {
        m_Rigidbody2D.gravityScale = 0f; // 0 gravity for a snappier response when trying to ascend from falling a distance
        m_Rigidbody2D.AddForce(new Vector2(0f, flySpeed));
    }
    if (hasFlying && !flying)
        {
            m_Rigidbody2D.gravityScale = gravityScale;  //adjust to a lighter gravity to simulate floating down
        }
  }


  private void Flip()
  {
    // Switch the way the player is labelled as facing.
    m_FacingRight = !m_FacingRight;

    transform.Rotate(0f, 180f, 0f);
  }
  public Rigidbody2D GetRigidBody2D()
  {
    return m_Rigidbody2D;
  }
    public Vector2 getDirection()
    {
        if (m_FacingRight)
        {
            return new Vector2(1,0);
        }
        else
        {
            return new Vector2(-1,0);
        }
    }
    public float getVelocityY()
    {
        return m_Rigidbody2D.velocity.y;
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