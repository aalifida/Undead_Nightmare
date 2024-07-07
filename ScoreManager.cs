using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public int currentScore;
    public static int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        PlayerPrefs.SetInt("currentScore",0 );
        UpdateHighScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            currentScore += 50;
            Destroy(other.gameObject, 0.5f);

            if (currentScore > highScore)
            {
                highScore = currentScore;
                UpdateHighScoreText();
                StoreHighScoreInPlayerPrefs(highScore);
            }

            UpdateScoreText();
            StoreScoreInPlayerPrefs(currentScore);
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
            
        }
        else
        {
            Debug.LogError("scoreText is not assigned!");
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
        else
        {
            Debug.LogError("highScoreText is not assigned!");
        }
    }

    public void StoreScoreInPlayerPrefs(int score)
    {
        PlayerPrefs.SetInt("currentScore", score);
        PlayerPrefs.Save();
    }

    public void StoreHighScoreInPlayerPrefs(int highScore)
    {
        PlayerPrefs.SetInt("highScore", highScore);
        PlayerPrefs.Save();
    }
}
