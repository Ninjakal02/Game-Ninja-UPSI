using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float jumpspeed;
    [SerializeField] private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //movement
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (horizontalInput > 0.01f) //detect right movement
        {
            transform.localScale = new Vector3(1, 1, 1); //face right
        }
        else if (horizontalInput < -0.01f) //Detect left movement
        {
            transform.localScale = new Vector3(-1, 1, 1); //Face left
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded);
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

}