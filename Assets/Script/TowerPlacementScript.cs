using UnityEngine;

public class TowerPlacementScript : MonoBehaviour
{
    [Header("Placement Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask buildablePlotLayer;

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
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, buildablePlotLayer);

            if (hit.collider != null)
            {
                BuildTile tile = hit.collider.GetComponent<BuildTile>();
                if (tile != null && tile.CanBuildHere())
                {
                    tile.Build(turretPrefab);
                    isPlacing = false;


                }
            }
        }
    }


    public void StartPlacing(GameObject selectedTurret)
    {
        turretPrefab = selectedTurret;
        isPlacing = true;
    }


}
