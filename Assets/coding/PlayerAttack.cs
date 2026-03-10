using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 20;
    public float attackPointDistance = 0.8f;

    [Header("Layers")]
    public LayerMask enemyLayer; // layer untuk musuh biasa

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Tukar posisi attackPoint ikut arah player
        if (sr.flipX)
        {
            attackPoint.localPosition = new Vector2(-attackPointDistance, 0);
        }
        else
        {
            attackPoint.localPosition = new Vector2(attackPointDistance, 0);
        }

        // Attack dengan left click
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
            Attack();
        }
    }

    void Attack()
    {
        // Detect semua collider dalam radius
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D obj in hitObjects)
        {
            // Musuh biasa
            EnemyHealth eh = obj.GetComponent<EnemyHealth>();
            if (eh != null)
            {
                eh.TakeDamage(attackDamage);
                Debug.Log("Hit Enemy: " + obj.name + " Damage: " + attackDamage);
            }

            // Boss
            if (obj.CompareTag("Boss"))
            {
                FinalBossHealth boss = obj.GetComponent<FinalBossHealth>();
                if (boss != null)
                {
                    boss.TakeDamage(attackDamage);
                    Debug.Log("Hit Boss: " + obj.name + " Damage: " + attackDamage);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}