using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    private Animator anim;  // animator enemy
    public float attackCooldown = 1f;
    private float lastAttackTime;
    void Start()
    {
        anim = GetComponentInParent<Animator>(); // ambil animator dari parent (enemy)
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(damage);
                // trigger animasi serang
                if (anim != null)
                {
                    anim.SetTrigger("Attack"); // pastikan animator ada Trigger "Attack"
                }
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
                anim.SetTrigger("Attack");
                lastAttackTime = Time.time;
            }
        }
    }
}

