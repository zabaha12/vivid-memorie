using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple2DCharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2; // Maximum number of jumps allowed
    public Animator animator;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveInput;
    private int remainingJumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        remainingJumps = maxJumps; // Initialize remaining jumps 
        rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    // Lock rotation around the z-axis
    rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    void Update()
    {
        // Handle input for horizontal movement
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Flip character sprite if moving in opposite direction
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Set animator parameter for running animation
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Check if the character can jump
        if (remainingJumps > 0)
        {
            // Perform the jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");

            // Decrease remaining jumps
            remainingJumps--;

            // If the character is on the ground, reset remaining jumps
            if (IsGrounded())
            {
                remainingJumps = maxJumps - 1; // Reset jumps to allow double jump
            }
        }
    }

    bool IsGrounded()
    {
        // Perform a raycast to check if the character is grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
