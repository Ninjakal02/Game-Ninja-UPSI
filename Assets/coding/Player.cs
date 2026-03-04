using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpspeed = 10f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move character left/right
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // Flip sprite based on direction
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        // Jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpspeed);
            isGrounded = false;
        }
    }

    // Detect landing on ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}