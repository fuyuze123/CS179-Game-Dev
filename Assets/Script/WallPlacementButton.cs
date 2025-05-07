using UnityEngine;
using UnityEngine.UI;

public class WallButton : MonoBehaviour
{
    [Header("Wall Prefab Reference")]
    [SerializeField] private GameObject wallPrefab;

    [Header("Attributes")]
    [SerializeField] private int wallCost = 20;

    private WallPlacementScript placementManager;

    private void Awake()
    {
        placementManager = FindFirstObjectByType<WallPlacementScript>();
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
        GoldRewarder.onGoldChange.AddListener(UpdateInteractability);
        UpdateInteractability(GoldRewarder.instance.GetCurrentGold());
    }

    private void OnButtonClick()
    {
        if (GoldRewarder.instance.GetCurrentGold() >= wallCost)
        {
            GoldRewarder.instance.ChangeGold(-wallCost);
            placementManager.StartPlacing(wallPrefab);
        }
    }

    private void UpdateInteractability(int currentGold)
    {
        GetComponent<Button>().interactable = (currentGold >= wallCost);
    }
}
