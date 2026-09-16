using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        Debug.Log("GAME PAUSED");
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        Debug.Log("GAME RESUMED");
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("mainmenu");
    }
}