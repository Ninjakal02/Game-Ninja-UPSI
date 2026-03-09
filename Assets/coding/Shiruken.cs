using UnityEngine;

public class Shiruken : MonoBehaviour
{
    public float speed = 10f;        // kelajuan terbang
    public float rotateSpeed = 600f; // kelajuan pusing
    public float destroyTime = 3f;   // masa sebelum hilang
    public int damage = 10;          // damage pada enemy

    private float direction = 1f;    // 1 = kanan, -1 = kiri
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, destroyTime); // auto destroy selepas masa tamat
    }

    // Set arah Shiruken
    public void SetDirection(float dir)
    {
        direction = dir;

        // Flip sprite ikut arah
        if (sr != null)
            sr.flipX = direction < 0;
    }

    void Update()
    {
        // Terbang lurus ikut direction
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction, Space.World);

        // Rotate
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // shiruken hilang bila kena enemy
        }
    }
}