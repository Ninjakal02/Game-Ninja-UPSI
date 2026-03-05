using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;

    int score = 0;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        scoreText.text = score.ToString();
    }

    public void Addpoint()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}