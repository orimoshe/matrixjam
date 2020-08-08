using UnityEngine;
using UnityEngine.Events;

namespace MatrixJam.Team18
{
    public class mainPlayerController : MonoBehaviour
    {
        Animator m_Animator;

        [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
        [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
        [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
        [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
        [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

        public mainPlayerController controller;
        public float runSpeed = 40f;

        private float horizontalMove = 0f;
        private bool jump = false;

        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private Vector3 m_Velocity = Vector3.zero;

        [Header("Events")]
        [Space]

        public UnityEvent OnLandEvent;

        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        private void Awake()
        {
            m_Animator = gameObject.GetComponent<Animator>();

            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            if (OnLandEvent == null)
                OnLandEvent = new UnityEvent();
        }

        private void Update()
        {
            horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            {
                m_Animator.SetBool("Walk", true);
            }
            else
            {
                m_Animator.SetBool("Walk", false);
            }
/*            if(Input.GetKey(KeyCode.W))
            {
                m_Animator.SetBool("Flip", true);
            }
            else
            {
                m_Animator.SetBool("Flip", false);
            }*/
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
            
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }


        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch)
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            //only control the player if grounded or airControl is turned on
            if (m_Grounded)
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
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    }