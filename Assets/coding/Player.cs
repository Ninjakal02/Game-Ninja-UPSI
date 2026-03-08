using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 10f;

    [Header("Shuriken")]
    [SerializeField] private GameObject shirukenPrefab;
    [SerializeField] private Transform throwPoint;

    private bool isGrounded;
    private bool isDead = false;

    public int maxShirukens = 3;
    private int shirukenCount = 0;

    private int facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        float horizontalInput = Input.GetAxis("Horizontal");

        // Movement
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // Flip
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            isGrounded = false;
        }

        // Throw Shuriken
        if (Input.GetMouseButtonDown(1) && shirukenCount < maxShirukens)
        {
            ThrowShiruken();
        }

        // Animator
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

    // ======================
    // SHURIKEN
    // ======================

    void ThrowShiruken()
    {
        GameObject shiruken = Instantiate(
            shirukenPrefab,
            throwPoint.position,
            Quaternion.identity
        );

        shiruken.GetComponent<Shiruken>().SetDirection(facingDirection);

        shirukenCount++;

        float destroyTime = shiruken.GetComponent<Shiruken>().destroyTime;

        Destroy(shiruken, destroyTime);

        StartCoroutine(ResetShiruken(destroyTime));
    }

    IEnumerator ResetShiruken(float delay)
    {
        yield return new WaitForSeconds(delay);
        shirukenCount--;
    }

    // ======================
    // DEATH
    // ======================

    void Die()
    {
        isDead = true;
        anim.SetBool("isDead", true);
    }
}