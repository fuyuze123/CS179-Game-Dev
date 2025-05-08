using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Button startWaveButton;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 12;
    [SerializeField] private float enemiesPerSecond = 2f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public static UnityEvent<int> onWaveChange = new UnityEvent<int>();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
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
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        enemiesAlive = 0;
        timeSinceLastSpawn = 0f;
        if (startWaveButton != null)
        {
            startWaveButton.interactable = false;
        }
        onWaveChange.Invoke(currentWave);
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        GoldRewarder.instance.ChangeGold(100);
        currentWave++;
        if (startWaveButton != null)
        {
            startWaveButton.interactable = true;
        }
        onWaveChange.Invoke(currentWave);
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManagingScript.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
