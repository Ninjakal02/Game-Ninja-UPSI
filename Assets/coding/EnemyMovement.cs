using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Obstacle Detection")]
    public Transform frontCheck;
    public Transform groundCheck;
    public float checkDistance = 0.6f;
    public float groundDistance = 0.2f;
    public float jumpForce = 7f;
    public float jumpCooldown = 1f;

    [Header("Layers")]
    public LayerMask obstacleLayer;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool movingRight = true;
    private bool chasing = false;
    private Transform player;
    private float lastJumpTime;

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
        float moveDir = 0f;

        if (chasing && player != null)
        {
            // Chase player
            moveDir = player.position.x > transform.position.x ? 1 : -1;
        }
        else
        {
            // Patrol
            moveDir = movingRight ? 1 : -1;

            if (movingRight && transform.position.x >= rightPoint.position.x)
                movingRight = false;
            else if (!movingRight && transform.position.x <= leftPoint.position.x)
                movingRight = true;
        }

        // Apply movement
        rb.linearVelocity = new Vector2(moveDir * speed, rb.linearVelocity.y);

        // Flip enemy sesuai arah sebenar
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (moveDir >= 0 ? 1 : -1);
        transform.localScale = scale;
    }

    void CheckObstacle()
    {
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        RaycastHit2D wall = Physics2D.Raycast(frontCheck.position, direction, checkDistance, obstacleLayer);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);

        Debug.DrawRay(frontCheck.position, direction * checkDistance, Color.red);
        Debug.DrawRay(groundCheck.position, Vector2.down * groundDistance, Color.blue);

        if (wall.collider != null && ground.collider != null && Time.time - lastJumpTime >= jumpCooldown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            lastJumpTime = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            chasing = true;
            player = other.transform;
        }
        if (other.CompareTag("JumpZone"))
        {
            // auto jump sekali
            if (rb != null)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            chasing = false;
            player = null;
        }
    }
}