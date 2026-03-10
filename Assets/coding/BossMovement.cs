using UnityEngine;

public class BossMovement : MonoBehaviour
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

    [Header("Boss Detection")]
    public float chaseRange = 8f;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;

    private Rigidbody2D rb;
    private Transform player;

    private bool movingRight = true;
    private bool chasing = false;

    private float moveDir = 1f;
    private float lastJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null)
            CheckChase();

        Move();
        CheckObstacle();
    }

    void CheckChase()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= chaseRange)
            chasing = true;
        else
            chasing = false;
    }

    void Move()
    {
        if (chasing && player != null)
        {
            // Kejar player
            moveDir = player.position.x > transform.position.x ? 1 : -1;
        }
        else
        {
            // Patrol kiri kanan
            moveDir = movingRight ? 1 : -1;

            if (movingRight && transform.position.x >= rightPoint.position.x)
                movingRight = false;
            else if (!movingRight && transform.position.x <= leftPoint.position.x)
                movingRight = true;
        }

        // Movement
        rb.linearVelocity = new Vector2(moveDir * speed, rb.linearVelocity.y);

        // Flip boss ikut arah gerakan
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (moveDir >= 0 ? 1 : -1);
        transform.localScale = scale;
    }

    void CheckObstacle()
    {
        Vector2 direction = moveDir > 0 ? Vector2.right : Vector2.left;

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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}