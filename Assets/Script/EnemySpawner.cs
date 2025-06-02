using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameDifficulty
{
    Beginner, Intermediate, Impossible
}

[System.Serializable]
public class WaveDefinition
{
    public int[] enemyTypeIndices;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject bossEnemyPrefab;
    [SerializeField] private Button startWaveButton;

    [Header("Wave Configuration")]
    [SerializeField] private List<WaveDefinition> waveCompositions;

    [Header("Attributes")]
    [SerializeField] private float enemiesPerSecond = 2f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public static UnityEvent<int> onWaveChange = new UnityEvent<int>();

    [Header("Game Difficulty")]
    public GameDifficulty currentDifficulty = GameDifficulty.Beginner;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    public bool isSpawning = false;
    private int currentEnemyOrderInWave;

    private void InitializeDefaultWaveCompositions()
    {
        waveCompositions = new List<WaveDefinition>();
        List<int> currentWaveData = new List<int>{0, 0, 0};
        waveCompositions.Add(new WaveDefinition{enemyTypeIndices = currentWaveData.ToArray()});

        for (int i = 2; i <= 50; i++)
        {
            List<int> previousLevelData = new List<int>(waveCompositions[waveCompositions.Count - 1].enemyTypeIndices);
            List<int> nextWaveData;

            if (i % 2 == 0)
            {
                nextWaveData = new List<int>(previousLevelData);
                if (nextWaveData.Count >= 2)
                {
                    nextWaveData[nextWaveData.Count - 1] = 1;
                    nextWaveData[nextWaveData.Count - 2] = 1;
                }
                else if (nextWaveData.Count == 1)
                {
                    nextWaveData[0] = 1;
                }
            }

            else
            {
                nextWaveData = new List<int>(previousLevelData);
                nextWaveData.Add(0);
                nextWaveData.Add(0);
                nextWaveData.Add(0);
            }

            waveCompositions.Add(new WaveDefinition{enemyTypeIndices = nextWaveData.ToArray()});
        }
    }

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);

        if (waveCompositions == null || waveCompositions.Count == 0)
        {
            InitializeDefaultWaveCompositions();
        }
    }

    private void Start()
    {
        if (startWaveButton != null)
        {
            startWaveButton.interactable = true;
        }
        onWaveChange.Invoke(currentWave);
    }

    private void Update()
    {
        if (!isSpawning)
        {
            return;
        }

        timeSinceLastSpawn += Time.deltaTime;
        if ((timeSinceLastSpawn >= (1f / enemiesPerSecond)) && (enemiesLeftToSpawn > 0))
        {
            Debug.Log("Spawn Enemy");
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if ((enemiesAlive == 0) && (enemiesLeftToSpawn == 0))
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    public void StartWave()
    {
        if (currentWave - 1 >= waveCompositions.Count)
        {
            isSpawning = false;
            if (startWaveButton != null)
            {
                startWaveButton.interactable = true;
            }
            return;
        }

        isSpawning = true;
        WaveDefinition currentWaveConfig = waveCompositions[currentWave - 1];
        enemiesLeftToSpawn = currentWaveConfig.enemyTypeIndices.Length;
        currentEnemyOrderInWave = 0;
        enemiesAlive = 0;

        bool isBossWave = currentWave % 5 == 0;
        timeSinceLastSpawn = 0f;

        if (isBossWave) 
        {
            enemiesLeftToSpawn--;
        }

        if (startWaveButton != null)
        {
            startWaveButton.interactable = false;
        }
        onWaveChange.Invoke(currentWave);

        if (isBossWave)
        {
            StartCoroutine(SpawnBossAtEnd());
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        GoldRewarder.instance.ChangeGold(100);
        currentWave++;

        if (currentWave > 50)
        {
            if (startWaveButton != null)
            {
                startWaveButton.interactable = false;
            }
            GameManager.instance.GameOver();
            return;
        }

        if (startWaveButton != null)
        {
            startWaveButton.interactable = true;
        }

        onWaveChange.Invoke(currentWave);
    }

    private void SpawnEnemy()
    {
        if (currentWave - 1 >= waveCompositions.Count) {
            return;
        }

        WaveDefinition currentWaveConfig = waveCompositions[currentWave - 1];

        if (currentEnemyOrderInWave >= currentWaveConfig.enemyTypeIndices.Length) {
            Debug.Log("Error: Out of Bounds in Array");
            return;
        }

        int baseEnemyIndex = currentWaveConfig.enemyTypeIndices[currentEnemyOrderInWave];
        int finalEnemyIndex = baseEnemyIndex;

        switch (currentDifficulty)
        {
            case GameDifficulty.Intermediate:
                if (baseEnemyIndex == 0)
                {
                    finalEnemyIndex = 1;
                }
                else if (baseEnemyIndex == 1)
                {
                    finalEnemyIndex = 2;
                }
                break;
            case GameDifficulty.Impossible:
                if (baseEnemyIndex == 0)
                {
                    finalEnemyIndex = 2;
                }
                else if (baseEnemyIndex == 1)
                {
                    finalEnemyIndex = 3;
                }
                break;
        }

        if (finalEnemyIndex < 0 || finalEnemyIndex >= enemyPrefabs.Length)
        {
            if (enemyPrefabs.Length > 0)
            {
                Instantiate(enemyPrefabs[0], LevelManagingScript.main.startPoint.position, Quaternion.identity);
            }
            else
            {
                currentEnemyOrderInWave++;
                return;
            }
        }
        else
        {
            GameObject prefabToSpawn = enemyPrefabs[finalEnemyIndex];
            Instantiate(prefabToSpawn, LevelManagingScript.main.startPoint.position, Quaternion.identity);
        }
        currentEnemyOrderInWave++;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public void SetWave(int wave)
    {
        currentWave = wave;
        onWaveChange.Invoke(currentWave);
    }

    private IEnumerator SpawnBossAtEnd() // special function type to help with boss delay
    {
        // Wait until all regular enemies are spawned
        while (enemiesLeftToSpawn > 0)
        {
            yield return null;
        }

        // Wait until regular enemies are spawned (in Update loop), then delay boss a bit
        yield return new WaitForSeconds(1f);

        Instantiate(bossEnemyPrefab, LevelManagingScript.main.startPoint.position, Quaternion.identity);
        enemiesAlive++;
    }
}
