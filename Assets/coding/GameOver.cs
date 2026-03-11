using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public GameObject gameOverUI;

    public void Die()
    {
        Debug.Log("Player Dead");

        gameOverUI.SetActive(true);

        Invoke("Respawn", 3f); // respawn selepas 3 saat
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}