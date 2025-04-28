using UnityEngine;
using UnityEngine.UI;

public class WallButton : MonoBehaviour
{
    [Header("Wall Prefab Reference")]
    [SerializeField] private GameObject wallPrefab;

    private WallPlacementScript placementManager;

    private void Awake()
    {
        placementManager = FindFirstObjectByType<WallPlacementScript>();

    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        placementManager.StartPlacing(wallPrefab);
    }
}
