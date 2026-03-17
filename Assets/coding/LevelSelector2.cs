using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelSelector2 : MonoBehaviour
{

    void start()
    {

    }
    public void OpenScene()
    {
        SceneManager.LoadScene("Level 2");
    }
}
