using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager :  MonoBehaviour
{
    [Header("UI Text Elements")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI healthText;

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
            UpdateWaveUI(FindFirstObjectByType<EnemySpawner>().GetCurrentWave());
        }
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
        waveText.text = "Level: " + newWaveAmount;
    }

    private void OnDestroy()
    {
        GoldRewarder.onGoldChange.RemoveListener(UpdateGoldUI);
        PlayerHealth.onPlayerHealthChange.RemoveListener(UpdateHealthUI);
    }
}