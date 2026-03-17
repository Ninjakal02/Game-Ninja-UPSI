using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelSelector1 : MonoBehaviour
{

    void start()
    {

    }
    public void OpenScene()
    {
        SceneManager.LoadScene("Gameplay level 1");
    }
}
