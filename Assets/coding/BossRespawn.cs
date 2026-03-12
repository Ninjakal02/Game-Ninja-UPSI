using UnityEngine;

[RequireComponent(typeof(FinalBossHealth))]
[RequireComponent(typeof(BossMovement))]
public class BossRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private FinalBossHealth health;
    private BossMovement movement;

    void Start()
    {
        startPosition = transform.position;
        health = GetComponent<FinalBossHealth>();
        movement = GetComponent<BossMovement>();
    }

    public void ResetBoss()
    {
        // Reset posisi
        transform.position = startPosition;

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