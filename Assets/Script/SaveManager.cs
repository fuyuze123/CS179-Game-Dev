using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private string savePath;
    public GameObject towerPrefab;
    public GameObject wallPrefab;
    public TowerPerkRegistry towerPerkRegistry;


    private void Awake()
    {
        if (instance == null) {
            instance = this;
            savePath = Application.persistentDataPath + "/save.json";
        } else {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        GameData data = new GameData();
        data.gold = GoldRewarder.instance.GetCurrentGold();
        data.playerHealth = PlayerHealth.instance.GetHealth();
        data.currentLevel = UIManager.instance.GetCurrentLevel();
        data.enemyDefeated = UIManager.instance.getCurrentDefeated();
        
        EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
        if (spawner != null)
        {
            data.selectedDifficulty = (int)spawner.currentDifficulty;
        }
        else
        {
            data.selectedDifficulty = (int)GameDifficulty.Beginner;
        }

        foreach (var tower in FindObjectsByType<Tower>(FindObjectsSortMode.None))
        {
            TowerSaveData towerData = new TowerSaveData();
            towerData.position = tower.transform.position;

            TowerUpgradeComponent upgrade = tower.GetComponent<TowerUpgradeComponent>();
            if (upgrade != null)
            {
                towerData.appliedPerks = upgrade.GetAppliedPerkNames();
            }

            data.towers.Add(towerData);
        }

        foreach (var wall in FindObjectsByType<WallHealth>(FindObjectsSortMode.None))
        {
            WallSaveData wallData = new WallSaveData();
            wallData.position = wall.transform.position;
            data.walls.Add(wallData);
        }

        File.WriteAllText(savePath, JsonUtility.ToJson(data, true));
        Debug.Log("Game saved to " + savePath);
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath)) {
            Debug.LogWarning("No save file found.");
            return;
        }

        GameData data = JsonUtility.FromJson<GameData>(File.ReadAllText(savePath));

        GoldRewarder.instance.ChangeGold(data.gold - GoldRewarder.instance.GetCurrentGold());
        PlayerHealth.instance.currentHealth = data.playerHealth;
        PlayerHealth.onPlayerHealthChange.Invoke(data.playerHealth);
        UIManager.instance.SetCurrentLevel(data.currentLevel);
        UIManager.instance.setCurrentDefeated(data.enemyDefeated);
        UIManager.instance.RefreshDefeatedEnemyUI();
        EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.SetWave(data.currentLevel);
            if (System.Enum.IsDefined(typeof(GameDifficulty), data.selectedDifficulty))
            {
                spawner.currentDifficulty = (GameDifficulty)data.selectedDifficulty;
                Debug.Log("Loaded difficulty: " + spawner.currentDifficulty);
            }
            else
            {
                spawner.currentDifficulty = GameDifficulty.Beginner;
            }
        }

        foreach (var tower in FindObjectsByType<Tower>(FindObjectsSortMode.None))
            Destroy(tower.gameObject);

        foreach (var wall in FindObjectsByType<WallHealth>(FindObjectsSortMode.None))
            Destroy(wall.gameObject);

        foreach (var td in data.towers)
        {
            GameObject tower = Instantiate(towerPrefab, td.position, Quaternion.identity); 
            TowerUpgradeComponent upgrade = tower.GetComponent<TowerUpgradeComponent>();
            if (upgrade != null && td.appliedPerks != null && td.appliedPerks.Count > 0)
            {
                upgrade.perkRegistry = towerPerkRegistry;
                upgrade.ApplyPerksByNames(td.appliedPerks);
            }
        }

        foreach (var wd in data.walls)
        {
            Instantiate(wallPrefab, wd.position, Quaternion.identity); 
        }

        Debug.Log("Game loaded.");

        GameObject startMenu = GameObject.Find("StartMenuPanel");
        if (startMenu != null)
            startMenu.SetActive(false);
    }
}
