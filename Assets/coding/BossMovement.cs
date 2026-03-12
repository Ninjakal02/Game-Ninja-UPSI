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
        chasing = distance <= chaseRange;
    }

    void Move()
    {
        // Tentukan arah
        if (chasing && player != null)
            moveDir = player.position.x > transform.position.x ? 1 : -1;
        else
        {
            moveDir = movingRight ? 1 : -1;

            if (movingRight && transform.position.x >= rightPoint.position.x)
                movingRight = false;
            else if (!movingRight && transform.position.x <= leftPoint.position.x)
                movingRight = true;
        }

        // Movement
        rb.linearVelocity = new Vector2(moveDir * speed, rb.linearVelocity.y);

        // Flip sprite ikut arah
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (moveDir >= 0 ? 1 : -1);
        transform.localScale = scale;
    }

    void CheckObstacle()
    {
        Vector2 direction = moveDir > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D wall = Physics2D.Raycast(frontCheck.position, direction, checkDistance, obstacleLayer);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);

        // Boss lompat bila ada obstacle & cooldown habis
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

        // ?? Lompat bila masuk JumpZone
        if (other.CompareTag("JumpZone"))
        {
            if (Time.time - lastJumpTime >= jumpCooldown)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                lastJumpTime = Time.time;
            }
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

    // ? Reset state selepas respawn
    public void ResetState()
    {
        movingRight = true;
        chasing = false;
        moveDir = 1f;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}