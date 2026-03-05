using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject levelPanel;  
    public Button start;  
    public Button level1;  
    public Button level2;  
    public Button level3;  
    public Button quit;   

    void Start()
    {
        levelPanel.SetActive(false);

        start.onClick.AddListener(OpenLevelPanel);
        level1.onClick.AddListener(() => LoadLevel("Level1"));
        level2.onClick.AddListener(() => LoadLevel("Level2"));
        level3.onClick.AddListener(() => LoadLevel("Level3"));
        quit.onClick.AddListener(QuitGame);  
    }

    void OpenLevelPanel()
    {
        levelPanel.SetActive(true);
        start.gameObject.SetActive(false);  
    }

    void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    void QuitGame()
    {
        Debug.Log("Quit Game"); 
        Application.Quit();     
    }
}