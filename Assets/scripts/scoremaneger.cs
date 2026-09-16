using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMPro.TMP_Text scoreText;

    private int score;
    private int highScore;

    public int Score => score;


    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateScoreUI();
    }


    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;
        }

        UpdateScoreUI();

        Debug.Log(
            "Score: " + score +
            " | High Score: " + highScore
        );
    }


    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "SCORE: " + score +
                "\nHIGH SCORE: " + highScore;
        }
    }


    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}