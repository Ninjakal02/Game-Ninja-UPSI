using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

            if (respawn != null)
            {
                respawn.SetCheckpoint(respawnPoint.position);
                Debug.Log("Checkpoint Updated!");
            }
        }
    }
}