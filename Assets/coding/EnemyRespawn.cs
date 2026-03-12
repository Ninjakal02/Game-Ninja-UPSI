using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMovement))]
public class EnemyRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    private EnemyHealth health;
    private EnemyMovement movement;

    void Start()
    {
        startPosition = transform.position;
        health = GetComponent<EnemyHealth>();
        movement = GetComponent<EnemyMovement>();
    }

    public void ResetEnemy()
    {
        // Reset posisi
        transform.position = startPosition;

        // Reset HP
        if (health != null)
            health.ResetHealth();

        // Reset movement state
        if (movement != null)
            movement.ResetState();

        // Aktifkan balik enemy
        gameObject.SetActive(true);
    }
}