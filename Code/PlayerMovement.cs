using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    public bool isGrounded = false;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;

    private float horizontalInput;
    private bool isFacingRight = true;
    private bool isSprinting;

    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        FlipSprite();

        // Ground check using raycast
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, checkRadius, groundLayer);
        bool wasGrounded = isGrounded;
        isGrounded = hit.collider != null;

          if (!wasGrounded && isGrounded) 
    {
        animator.SetBool("isJumping", false);
    }

        // Debug line to visualize the ground check
        Debug.DrawRay(groundCheck.position, Vector2.down * checkRadius, isGrounded ? Color.green : Color.red);

        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    void FixedUpdate()
    {
        float speed = isSprinting ? sprintSpeed : walkSpeed;
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Update animator parameters
        animator.SetFloat("xVelocity", Mathf.Abs(horizontalInput * speed));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void FlipSprite()
    {
        if (horizontalInput > 0f && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0f && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 ls = transform.localScale;
        ls.x *= -1f;
        transform.localScale = ls;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkRadius);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is on the ground layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}



