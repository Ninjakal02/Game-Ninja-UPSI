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

    [Header("Smoke Effect")]
    public GameObject smokeEffectPrefab; // letak prefab particle system

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

        rb.linearVelocity = new Vector2(moveDir * speed, rb.linearVelocity.y);

        // flip sprite ikut arah
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (moveDir >= 0 ? 1 : -1);
        transform.localScale = scale;
    }

    void CheckObstacle()
    {
        Vector2 direction = moveDir > 0 ? Vector2.right : Vector2.left;

        RaycastHit2D wall = Physics2D.Raycast(frontCheck.position, direction, checkDistance, obstacleLayer);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, groundLayer);

        if (wall.collider != null && ground.collider != null && Time.time - lastJumpTime >= jumpCooldown)
        {
            JumpWithSmoke();
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
            if (Time.time - lastJumpTime >= jumpCooldown)
            {
                JumpWithSmoke();
            }
        }
    }

    void JumpWithSmoke()
    {
        // lompat
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        lastJumpTime = Time.time;

        // spawn smoke effect kat atas boss
        if (smokeEffectPrefab != null)
        {
            Vector3 spawnPos = transform.position + new Vector3(0f, 0.5f, 0f);
            GameObject smoke = Instantiate(smokeEffectPrefab, spawnPos, Quaternion.identity);

            // pastikan particle system start
            ParticleSystem ps = smoke.GetComponent<ParticleSystem>();
            if (ps != null) ps.Play();

            // optional: auto destroy selepas 1-2 saat
            Destroy(smoke, 2f);
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