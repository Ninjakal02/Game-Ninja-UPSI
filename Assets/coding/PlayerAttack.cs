using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    public Transform attackPoint;
    public float attackRange = 1f;
    public int attackDamage = 20;

    public LayerMask enemyLayer;

    public float attackPointDistance = 0.8f;

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

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
            Attack();
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayer
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth eh = enemy.GetComponent<EnemyHealth>();

            if (eh != null)
            {
                eh.TakeDamage(attackDamage);
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