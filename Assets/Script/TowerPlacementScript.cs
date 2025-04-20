using UnityEngine;

public class TowerPlacementScript : MonoBehaviour
{
    [Header("Placement Settings")]
    [SerializeField] private Camera mainCamera;

    private GameObject turretPrefab;
    private bool isPlacing = false;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (isPlacing && Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(turretPrefab, worldPos, Quaternion.identity);
            isPlacing = false;
        }
    }

    public void StartPlacing(GameObject selectedTurret)
    {
        turretPrefab = selectedTurret;
        isPlacing = true;
    }


}
