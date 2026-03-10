using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private Animator anim;
    [Header("Audio")]
    [SerializeField] private AudioClip enemyAttackClip;
    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        audioSource = GetComponentInParent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                if (anim != null)
                    anim.SetTrigger("Attack");

                if (enemyAttackClip != null && audioSource != null)
                    audioSource.PlayOneShot(enemyAttackClip);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
                if (anim != null)
                    anim.SetTrigger("Attack");

                if (enemyAttackClip != null && audioSource != null)
                    audioSource.PlayOneShot(enemyAttackClip);

                lastAttackTime = Time.time;
            }
        }
    }
}