using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public bool levelCompleted = false;

    private void Awake()
    {
        Instance = this;
    }

    public void CompleteLevel()
    {
        levelCompleted = true;
        Debug.Log("Level Completed!");

        Invoke("LoadNext", 3f);
    }

    void LoadNext()
    {
        GameManager.Instance.NextLevel();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
    }
}