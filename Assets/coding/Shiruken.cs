using UnityEngine;

public class Shiruken : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 600f;
    public float destroyTime = 3f;
    public int damage = 10;

    [Header("Audio")]
    [SerializeField] private AudioClip hitEnemyClip;
    private AudioSource audioSource;

    private float direction = 1f; // 1 = kanan, -1 = kiri
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = gameObject.AddComponent<AudioSource>();
        Destroy(gameObject, destroyTime);
    }

    public void SetDirection(float dir)
    {
        direction = dir;

        if (sr != null)
            sr.flipX = direction < 0;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction, Space.World);
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

                if (hitEnemyClip != null)
                    audioSource.PlayOneShot(hitEnemyClip);
            }

            Destroy(gameObject);
        }
    }
}