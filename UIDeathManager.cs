using UnityEngine;
using TMPro;

public class UIDeathManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI coinText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("currentScore");
        int highScore = PlayerPrefs.GetInt("highScore");
        int coins = PlayerPrefs.GetInt("coins");

        int earnedCoins = score / 5;
        coins += earnedCoins;

        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
        coinText.text = coins.ToString();

        
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.Save();
    }
}
