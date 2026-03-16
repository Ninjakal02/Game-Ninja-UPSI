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

        // spawn awal jadi checkpoint pertama
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

        // reset position
        transform.position = checkpointPosition;

        // reset physics
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // reset animation
        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
            anim.SetBool("isDead", false);
        }

        // reset semua enemy
        EnemyRespawn[] enemies = FindObjectsByType<EnemyRespawn>(FindObjectsSortMode.None);
        foreach (EnemyRespawn enemy in enemies)
        {
            enemy.ResetEnemy();
        }

        // reset boss
        BossRespawn boss = FindObjectOfType<BossRespawn>();
        if (boss != null)
        {
            boss.ResetBoss();
        }
    }
}