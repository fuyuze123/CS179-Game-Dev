using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject howToPanel;

    private static bool hasStartedGame = false;


    void Start()
    {
        if (!hasStartedGame)
        {
            // Show start menu and pause game on fresh load
            startMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            // Hide start menu on replay
            startMenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void StartGame()
    {
        hasStartedGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowHowTo()
    {
        startMenuPanel.SetActive(false);
        howToPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        hasStartedGame = false;
        howToPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
