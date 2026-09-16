using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 180f;

    [Header("UI")]
    [SerializeField] private TMPro.TMP_Text timerText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Score")]
    [SerializeField] private ScoreManager scoreManager;

    private float timeRemaining;
    private bool gameRunning;
    public bool IsGameRunning => gameRunning;

    private void Start()
    {
        StartGame();
    }


    private void Update()
    {
        if (!gameRunning)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGame();
        }

        UpdateTimerUI();
    }


    public void StartGame()
    {
        timeRemaining = gameDuration;
        gameRunning = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        UpdateTimerUI();

        Debug.Log("Game Started!");
    }


    private void EndGame()
    {
        Time.timeScale = 1f;
        gameRunning = false;

        if (scoreManager != null)
        {
            scoreManager.SaveHighScore();
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        UpdateTimerUI();

        Debug.Log("GAME OVER!");
    }


    public void PlayAgain()
    {
        Debug.Log("PLAY AGAIN BUTTON CLICKED!");

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }


    private void UpdateTimerUI()
    {
        if (timerText == null)
            return;

        int minutes =
            Mathf.FloorToInt(timeRemaining / 60f);

        int seconds =
            Mathf.FloorToInt(timeRemaining % 60f);

        timerText.text =
            string.Format(
                "TIME: {0:00}:{1:00}",
                minutes,
                seconds
            );
    }
}