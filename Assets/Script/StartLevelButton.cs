using UnityEngine;
using UnityEngine.UI;

public class StartLevelButton : MonoBehaviour
{
    [Header("Enemy Spawner Reference")]
    [SerializeField] private EnemySpawner enemySpawner;

    private void Awake()
    {
        if (enemySpawner == null)
        {
            enemySpawner = FindFirstObjectByType<EnemySpawner>();
        }
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (enemySpawner != null)
        {
            enemySpawner.StartWave();
        }
    }
}
