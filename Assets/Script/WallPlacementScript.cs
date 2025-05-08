using UnityEngine;

public class WallPlacementScript : MonoBehaviour
{
    [Header("Placement Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask buildableGroundLayer;
    [SerializeField] private LayerMask buildTileLayer;

    private GameObject wallPrefab;
    private bool isPlacing = false;
    private Vector2 currentWallColliderSize;

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
            if (wallPrefab == null)
            {
                Debug.LogError("WallPrefab is not set in WallPlacementScript.cs. Cannot place wall!");
                isPlacing = false;
                return;
            }
            Vector2 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, Mathf.Infinity, buildableGroundLayer);

            if (hit.collider != null)
            {
                Collider2D buildTileCollider = Physics2D.OverlapBox(worldPos, currentWallColliderSize, 0f, buildTileLayer);
                if (buildTileCollider == null)
                {
                    Instantiate(wallPrefab, worldPos, Quaternion.identity);
                    isPlacing = false;
                }
            }
        }
    }


    public void StartPlacing(GameObject selectedWall)
    {
        wallPrefab = selectedWall;
        BoxCollider2D wallCollider = selectedWall.GetComponent<BoxCollider2D>();
        if (wallCollider != null)
        {
            currentWallColliderSize = wallCollider.size;
        }
        isPlacing = true;
    }


}
