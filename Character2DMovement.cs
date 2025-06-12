using UnityEngine;

public class Character2DMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("Rigidbody2D not found!");
        if (animator == null)
            Debug.LogWarning("Animator component not found!");
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (animator != null)
        {
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("Speed", Mathf.Abs(movement.x));
            animator.SetBool("isJumping", !isGrounded);
        }
    }
    void FixedUpdate()
    {
       
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }
}
