using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;

        if (level == 1)
            SceneManager.LoadScene("Level1");
        else if (level == 2)
            SceneManager.LoadScene("Level2");
        else if (level == 3)
            SceneManager.LoadScene("Level3");
    }

    public void NextLevel()
    {
        currentLevel++;

        if (currentLevel <= 3)
            LoadLevel(currentLevel);
        else
            Debug.Log("GAME COMPLETED");
    }
}