using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    [Header("Turret Prefab Reference")]
    [SerializeField] private GameObject turretPrefab;

    [Header("Attributes")]
    [SerializeField] private int turretCost = 50;

    private TowerPlacementScript placementManager;

    private void Awake()
    {
        placementManager = FindFirstObjectByType<TowerPlacementScript>();
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
        GoldRewarder.onGoldChange.AddListener(UpdateInteractability);
        UpdateInteractability(GoldRewarder.instance.GetCurrentGold());
    }

    private void OnButtonClick()
    {
        if (GoldRewarder.instance.GetCurrentGold() >= turretCost)
        {
            GoldRewarder.instance.ChangeGold(-turretCost);
            placementManager.StartPlacing(turretPrefab);
        }
    }

    private void UpdateInteractability(int currentGold)
    {
        GetComponent<Button>().interactable = (currentGold >= turretCost);
    }
}
