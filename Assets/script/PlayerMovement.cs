using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public fields
    [SerializeField] float speed = 4;
    [SerializeField] float jumpPower = 500;
    [SerializeField] Transform groundCheckCollider;
    const float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    // Private Fields
    private Rigidbody2D rb;
    private float horizontalValue;
    private bool facingRight = true;
    private Vector3 initialScale;
    private Animator anim;
    private bool Jump = false;
    [SerializeField] private bool isGrounded = false;
    private SoundManager soundManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale; // Store the initial scale
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Lock z-axis rotation
        anim = GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        anim.SetFloat("yVelocity", rb.velocity.y);
        horizontalValue = Input.GetAxisRaw("Horizontal");
        Debug.Log(horizontalValue);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            soundManager.PlayJumpSound(); // Play jump sound
            Jump = true;
            anim.SetBool("Jump", true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            Jump = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            soundManager.PlayHitSound(); // Play hit sound
            hit();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            soundManager.PlayKickSound(); // Play kick sound
            kick();
        }
    
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue, Jump);
    }

    void Move(float dir, bool jumpFlag)
    {
        if (isGrounded && jumpFlag)
        {
            isGrounded = false;
            jumpFlag = false;
            rb.AddForce(new Vector2(0f, jumpPower));
        }

        float xVal = dir * speed * 150 * Time.fixedDeltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        // Flip character direction if necessary
        if ((facingRight && dir < 0) || (!facingRight && dir > 0))
        {
            FlipCharacterDirection();
        }
    }

    void FlipCharacterDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(facingRight ? initialScale.x : -initialScale.x, initialScale.y, initialScale.z);
    }

    void GroundCheck()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;
        anim.SetBool("Jump", !isGrounded);
    }

    void hit()
    {
        anim.SetTrigger("hit");
    }

    void kick()
    {
        anim.SetTrigger("kick");
    }
}
