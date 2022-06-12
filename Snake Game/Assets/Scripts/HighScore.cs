using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private int newScore;
    private int highScore;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI currentScoreText;

    void Start()
    {
        newScore = PlayerPrefs.GetInt("Score");
        HighScoreSet();
    }

    void Update()
    {
        currentScoreText.text = "Your Score: " + newScore.ToString();
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("hiScore").ToString();
    }
    void HighScoreSet()
    {
        if (PlayerPrefs.HasKey("hiScore"))
        {
            if (newScore > PlayerPrefs.GetInt("hiScore"))
            {
                highScore = newScore;
                PlayerPrefs.SetInt("hiScore", highScore);
                PlayerPrefs.Save();
            }
        }
        else
        {
            if (newScore > highScore)
            {
                highScore = newScore;
                PlayerPrefs.SetInt("hiScore", highScore);
                PlayerPrefs.Save();
            }
        }
    }
}

