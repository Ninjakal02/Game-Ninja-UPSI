using UnityEngine;

[RequireComponent(typeof(FinalBossHealth))]
[RequireComponent(typeof(BossMovement))]
public class BossRespawn : MonoBehaviour
{
    private FinalBossHealth health;
    private BossMovement movement;

    [Header("Spawn Zone (Required)")]
    public BoxCollider2D spawnZone; // letak Box Collider 2D untuk zone respawn

    void Start()
    {
        if (spawnZone == null)
            Debug.LogWarning("BossRespawn: Spawn Zone belum assign!");

        health = GetComponent<FinalBossHealth>();
        movement = GetComponent<BossMovement>();
    }

    public void ResetBoss()
    {
        // Respawn dalam spawn zone
        if (spawnZone != null)
        {
            Vector3 newPos = new Vector3(
                Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x),
                Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y),
                transform.position.z
            );

            transform.position = newPos;
        }

        // Reset HP
        if (health != null)
            health.ResetHealth();

        // Reset movement
        if (movement != null)
            movement.ResetState();

        // Aktifkan balik boss
        gameObject.SetActive(true);
    }
}