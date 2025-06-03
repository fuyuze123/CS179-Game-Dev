using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private int currentLevel = 1;
    private int currentEnemyDefeated = 0;
    [Header("UI Text Elements")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI defeatedEnemyText;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Button Back_to_Menu_Button;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (GoldRewarder.instance != null)
        {
            GoldRewarder.onGoldChange.AddListener(UpdateGoldUI);
            UpdateGoldUI(GoldRewarder.instance.GetCurrentGold());
        }
        if (PlayerHealth.instance != null)
        {
            PlayerHealth.onPlayerHealthChange.AddListener(UpdateHealthUI);
            UpdateHealthUI(PlayerHealth.instance.GetHealth());
        }
        if (FindFirstObjectByType<EnemySpawner>() != null)
        {
            EnemySpawner.onWaveChange.AddListener(UpdateWaveUI);
            defeatedEnemyText.text = "Kill: " + currentEnemyDefeated;
            UpdateWaveUI(FindFirstObjectByType<EnemySpawner>().GetCurrentWave());
        }
        if (gameOverText != null)
        {
            gameOverText.SetActive(false); // Hide it at the beginning
        }
    }
    public void UpdateEnemyDefeatedUI()
    {
        currentEnemyDefeated++;
         defeatedEnemyText.text = "Kill: " + currentEnemyDefeated;
    }

    private void UpdateGoldUI(int newGoldAmount)
    {
        goldText.text = "Gold: " + newGoldAmount;
    }

    private void UpdateHealthUI(int newHealthAmount)
    {
        healthText.text = "Health: " + newHealthAmount;
    }

    private void UpdateWaveUI(int newWaveAmount)
    {
        currentLevel = newWaveAmount;
        waveText.text = "Level: " + newWaveAmount;
    }

    private void OnDestroy()
    {
        GoldRewarder.onGoldChange.RemoveListener(UpdateGoldUI);
        PlayerHealth.onPlayerHealthChange.RemoveListener(UpdateHealthUI);
    }

    public void ShowGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
        if (Back_to_Menu_Button != null)
        {
            Back_to_Menu_Button.gameObject.SetActive(true);
        }
        
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
        waveText.text = "Level: " + level;
    }

    public int getCurrentDefeated()
    {
        return currentEnemyDefeated;
    }
    public void setCurrentDefeated(int newEnemyDefeated)
    {
        currentEnemyDefeated = newEnemyDefeated;

    }

    public void RefreshDefeatedEnemyUI()
    {
        defeatedEnemyText.text = "Kill: " + currentEnemyDefeated;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // just in case it was paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}