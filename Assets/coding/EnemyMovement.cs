using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;

    public Transform leftPoint;
    public Transform rightPoint;

    public Transform frontCheck;
    public Transform groundCheck;

    public float checkDistance = 0.6f;
    public float groundDistance = 0.2f;

    public float jumpForce = 7f;

    public LayerMask obstacleLayer;
    public LayerMask groundLayer;


    private bool movingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        CheckObstacle();
    }

    void Move()
    {
        float direction = movingRight ? 1 : -1;

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if (movingRight && transform.position.x >= rightPoint.position.x)
        {
            movingRight = false;
            Flip();
        }

        if (!movingRight && transform.position.x <= leftPoint.position.x)
        {
            movingRight = true;
            Flip();
        }
    }

    void CheckObstacle()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        RaycastHit2D wall = Physics2D.Raycast(frontCheck.position, direction, checkDistance, obstacleLayer);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);

        Debug.DrawRay(frontCheck.position, direction * checkDistance, Color.red);
        Debug.DrawRay(groundCheck.position, Vector2.down * groundDistance, Color.blue);

        if (wall.collider != null && ground.collider != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}