using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    private bool wasGrounded;

    [HideInInspector]
    public bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        CheckGround();
        ProcessMovement();
        Animate();
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void ProcessMovement()
    {
       float moveInput = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("isJumping", true); // Indicás salto cuando se inicia el salto
        }

        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
    }
    

    void Animate()
    {
         animator.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));

    // Detectar aterrizaje
    if (!wasGrounded && isGrounded)
    {
        // Acaba de aterrizar
        animator.SetBool("isJumping", false);
    }
    else if (!isGrounded)
    {
        // Está en el aire
        animator.SetBool("isJumping", true);
    }
    else if (isGrounded)
    {
        // En el suelo y no está aterrizando justo ahora
        animator.SetBool("isJumping", false);
    }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");

        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
