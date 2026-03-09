using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpSpeed = 10f;

    [Header("Shiruken")]
    [SerializeField] private GameObject shirukenPrefab;
    [SerializeField] private Transform throwPoint;

    [Header("Shiruken Limit")]
    public int maxShirukens = 10;
    private int shirukenCount = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI shirukenText;

    private bool isGrounded;
    private bool isDead = false;

    private int facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        UpdateShirukenUI();
    }

    void Update()
    {
        if (isDead) return;

        float horizontalInput = Input.GetAxis("Horizontal");

        // ======================
        // MOVEMENT
        // ======================
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        // Flip character
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

        // ======================
        // JUMP
        // ======================
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            isGrounded = false;
        }

        // ======================
        // THROW SHIRUKEN
        // ======================
        if (Input.GetMouseButtonDown(1))
        {
            ThrowShiruken();
        }

        // ======================
        // ANIMATION
        // ======================
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
    // SHIRUKEN SYSTEM
    // ======================

    void ThrowShiruken()
    {
        if (shirukenCount >= maxShirukens)
        {
            Debug.Log("Shiruken habis!");
            return;
        }

        GameObject shiruken = Instantiate(
            shirukenPrefab,
            throwPoint.position,
            Quaternion.identity
        );

        shiruken.GetComponent<Shiruken>().SetDirection(facingDirection);

        shirukenCount++;

        UpdateShirukenUI();
    }

    void UpdateShirukenUI()
    {
        if (shirukenText != null)
        {
            shirukenText.text = "Shuriken: " + (maxShirukens - shirukenCount) + " / " + maxShirukens;
        }
    }

    // ======================
    // PLAYER DEATH
    // ======================

    void Die()
    {
        isDead = true;
        anim.SetBool("isDead", true);
    }
}