using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    [Header("Turret Prefab Reference")]
    [SerializeField] private GameObject turretPrefab;

    private TowerPlacementScript placementManager;

    private void Awake()
    {
        placementManager = FindFirstObjectByType<TowerPlacementScript>();

    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        placementManager.StartPlacing(turretPrefab);
    }
}
