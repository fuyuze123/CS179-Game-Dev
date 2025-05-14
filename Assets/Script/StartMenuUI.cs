using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject howToPanel;

    public void StartGame()
    {
        startMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Resume time if paused
    }

    public void ShowHowTo()
    {
        startMenuPanel.SetActive(false);
        howToPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        howToPanel.SetActive(false);
        startMenuPanel.SetActive(true);
    }
}
