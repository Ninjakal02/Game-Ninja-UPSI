using UnityEngine;
using UnityEngine.UI;

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

    [Header("Shiruken Limit & UI")]
    public int maxShirukens = 10;
    private int shirukenCount = 0;
    [SerializeField] private Image[] shirukenIcons;

    [Header("Audio")]
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip throwClip;
    private AudioSource audioSource;

    private bool isGrounded;
    private bool isDead = false;

    public bool canMove = true; // UNTUK FREEZE PLAYER

    private int facingDirection = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        ResetShirukenIcons();
    }

    void Update()
    {
        if (isDead) return;

        float horizontalInput = 0;

        if (canMove)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
                isGrounded = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Attack");

                if (attackClip != null && audioSource != null)
                    audioSource.PlayOneShot(attackClip);
            }

            if (Input.GetMouseButtonDown(1))
            {
                ThrowShiruken();
            }
        }

        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

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

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void ThrowShiruken()
    {
        if (shirukenCount >= maxShirukens)
        {
            Debug.Log("Shiruken habis!");
            return;
        }

        anim.SetTrigger("Throw");

        if (throwClip != null && audioSource != null)
            audioSource.PlayOneShot(throwClip);

        GameObject shiruken = Instantiate(shirukenPrefab, throwPoint.position, Quaternion.identity);
        shiruken.GetComponent<Shiruken>().SetDirection(facingDirection);

        if (shirukenCount < shirukenIcons.Length)
            shirukenIcons[shirukenCount].enabled = false;

        shirukenCount++;
    }

    public void ResetShirukenIcons()
    {
        for (int i = 0; i < shirukenIcons.Length; i++)
            shirukenIcons[i].enabled = true;

        shirukenCount = 0;
    }

    public void Die()
    {
        isDead = true;
        anim.SetBool("isDead", true);
    }
}