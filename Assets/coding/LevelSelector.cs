using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelSelector : MonoBehaviour
{

    void start()
    {

    }
    public void OpenScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
