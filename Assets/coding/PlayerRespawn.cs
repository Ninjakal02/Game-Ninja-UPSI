using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPoint;

    void Start()
    {
        respawnPoint = transform.position; // spawn awal
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
    }
}