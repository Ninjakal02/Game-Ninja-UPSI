using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpspeed = 10f;

    [SerializeField] private GameObject shurikenPrefab;
    [SerializeField] private Transform throwPoint;

    private bool isGrounded;
    private bool isDead = false;

    // Shuriken limit
    public int maxShurikens = 3;
    private int shurikenCount = 0;

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

        // Flip character menggunakan flipX
        if (horizontalInput > 0.01f)
            sr.flipX = false;  // kanan
        else if (horizontalInput < -0.01f)
            sr.flipX = true;   // kiri

        // Jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpspeed);
            isGrounded = false;
        }

        // Throw shuriken animation
        if (Input.GetKeyDown(KeyCode.F) && !isDead && shurikenCount < maxShurikens)
        {
            anim.SetTrigger("throw");
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

    // Spawn shuriken (dipanggil dari animation event "ThrowShuriken")
    public void ThrowShuriken()
    {
        if (shurikenCount >= maxShurikens) return; // limit shuriken

        GameObject shuriken = Instantiate(shurikenPrefab, throwPoint.position, Quaternion.identity);

        // Direction ikut flipX
        float direction = sr.flipX ? -1f : 1f;
        shuriken.GetComponent<Shuriken>().SetDirection(direction);

        shurikenCount++;
        float destroyTime = shuriken.GetComponent<Shuriken>().destroyTime;
        Destroy(shuriken, destroyTime);

        StartCoroutine(ResetShurikenCount(destroyTime));
    }

    IEnumerator ResetShurikenCount(float delay)
    {
        yield return new WaitForSeconds(delay);
        shurikenCount--;
    }
}