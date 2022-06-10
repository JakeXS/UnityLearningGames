using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int p1score = 0;
    private int p2score = 0;
    public int scoreToReach;
    public TextMeshProUGUI p1SCText;
    public TextMeshProUGUI p2SCText;

    public void P1Goal()
    {
        p1score++;
        p1SCText.text = p1score.ToString();
        CheckScore();
    }
    public void P2Goal()
    {
        p2score++;
        p2SCText.text = p2score.ToString();
        CheckScore();
    }

    public void CheckScore()
    {
        if (p1score == scoreToReach || p2score == scoreToReach)
        {
            SceneManager.LoadScene(2);
        }
        
    }
}
