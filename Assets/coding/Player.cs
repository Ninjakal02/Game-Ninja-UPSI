using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpspeed = 10f;

    [SerializeField] private GameObject ShirukenPrefab;  // prefab Shiruken
    [SerializeField] private Transform throwPoint;

    private bool isGrounded;
    private bool isDead = false;

    // Shiruken limit
    public int maxShirukens = 3;
    private int shirukenCount = 0;

    // Track last facing direction (1 = kanan, -1 = kiri)
    private int facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move character
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // Flip character & update facingDirection
        if (horizontalInput > 0.01f)
        {
            sr.flipX = false;
            facingDirection = 1;
        }
        else if (horizontalInput < -0.01f)
        {
            sr.flipX = true;
            facingDirection = -1;
        }

        // Jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpspeed);
            isGrounded = false;
        }

        // DEBUG TEST: Throw Shiruken tanpa animasi
        if (Input.GetMouseButtonDown(1) && !isDead && shirukenCount < maxShirukens)
        {
            ThrowShiruken();
        }

        // Attack animation
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            anim.SetTrigger("Attack");
        }

        // Test death
        if (Input.GetKeyDown(KeyCode.K))
        {
            isDead = true;
            anim.SetBool("isDead", true);
        }

        // Animator parameters
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Spawn Shiruken
    public void ThrowShiruken()
    {
        if (shirukenCount >= maxShirukens) return;

        GameObject shiruken = Instantiate(ShirukenPrefab, throwPoint.position, Quaternion.identity);

        // Debug log
        Debug.Log("FacingDirection: " + facingDirection + " | SpawnPos: " + throwPoint.position);

        // Set direction
        shiruken.GetComponent<Shiruken>().SetDirection(facingDirection);

        shirukenCount++;
        float destroyTime = shiruken.GetComponent<Shiruken>().destroyTime;
        Destroy(shiruken, destroyTime);

        StartCoroutine(ResetShirukenCount(destroyTime));
    }

    IEnumerator ResetShirukenCount(float delay)
    {
        yield return new WaitForSeconds(delay);
        shirukenCount--;
    }

    // Gizmo untuk nampak ThrowPoint
    void OnDrawGizmos()
    {
        if (throwPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(throwPoint.position, 0.05f);
        }
    }
}