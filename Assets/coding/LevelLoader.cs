using UnityEngine;
using UnityEngine.SceneManagement;  // penting untuk load scene

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private string nextLevelName; // masukkan nama scene target dalam inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        // pastikan player yang trigger
        if (other.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}