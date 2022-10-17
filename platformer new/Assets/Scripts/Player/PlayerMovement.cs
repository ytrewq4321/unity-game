using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 20f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private int playerLayer;
    private int platformLayer;

    public int JumpCount = 0;
    public float NormalGravity;
    public bool IsJumping=false;
    public float MoveHorizontal;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        playerLayer = LayerMask.NameToLayer("Player");
        platformLayer = LayerMask.NameToLayer("Platform");
        NormalGravity = rb.gravityScale;
    }

    private void Update()
    {
        MoveHorizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(MoveHorizontal));
        if (Input.GetButtonDown("Jump") && (!IsJumping||JumpCount<2))
            Jump();

        JumpOnPlatform();
    }

    private void FixedUpdate()
    {
        if (MoveHorizontal != 0)
            Run();
    }

    private void Run()
    {
        sprite.flipX = MoveHorizontal > 0f;
        rb.AddForce(new Vector2(MoveHorizontal*moveSpeed, 0f), ForceMode2D.Impulse);   
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        JumpCount++;
    }

    private void JumpOnPlatform()
    {
        if (rb.velocity.y > 0)
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, true);
        else
            Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground" )
        {
            IsJumping = false;
            JumpCount = 0;
            animator.SetBool("IsJumping", false);
        }     
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            IsJumping = true;
            animator.SetBool("IsJumping", true);
        }
    }
}

