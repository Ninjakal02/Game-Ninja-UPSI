using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public float reloadDelay = 2f;

    public void ReloadCurrentScene()
    {
        Invoke("ReloadSceneNow", reloadDelay);
    }

    void ReloadSceneNow()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}