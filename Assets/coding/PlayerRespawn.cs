using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 checkpointPosition;
    private Rigidbody2D rb;
    private Animator anim;

    public int maxRespawn = 2;
    private int currentRespawn = 0;

    private GameOverManager gameOverManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        checkpointPosition = transform.position;

        gameOverManager = FindObjectOfType<GameOverManager>();
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
        Debug.Log("Checkpoint Updated!");
    }

    public void Respawn()
    {
        currentRespawn++;

        Debug.Log("Respawn count: " + currentRespawn);

        // kalau lebih 3 kali ? game over
        if (currentRespawn > maxRespawn)
        {
            if (gameOverManager != null)
                gameOverManager.ShowGameOver();

            gameObject.SetActive(false);
            return;
        }

        // Respawn Player
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

        // reset enemy
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