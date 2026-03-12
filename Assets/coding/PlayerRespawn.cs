using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 checkpointPosition;
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // checkpoint awal = spawn point
        checkpointPosition = transform.position;
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
        Debug.Log("Checkpoint Updated!");
    }

    public void Respawn()
    {
        Debug.Log("Player Respawn");

        // 1?? Respawn Player
        transform.position = checkpointPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
            anim.SetBool("isDead", false);
        }

        // 2?? Reset Semua Enemy
        EnemyRespawn[] enemies = FindObjectsOfType<EnemyRespawn>();
        foreach (EnemyRespawn enemy in enemies)
        {
            enemy.ResetEnemy();
        }

        // 3?? Reset Boss
        BossRespawn boss = FindObjectOfType<BossRespawn>();
        if (boss != null)
        {
            boss.ResetBoss();
        }
    }
}