using UnityEngine;
using UnityEngine.EventSystems;

public class TowerClickDetector : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] public TowerUpgradePanel upgradePanel;
    [SerializeField] public TowerSelectionManager selectionManager;

    [SerializeField] private Color highlightColor = Color.yellow;
    private Color ogColor;
    private SpriteRenderer sr;

    private void initialization()
    {
     

        if (mainCamera == null){ mainCamera = Camera.main;}
        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
            ogColor = sr != null ? sr.color : Color.white;
        }
        if (upgradePanel == null) {upgradePanel = FindFirstObjectByType<TowerUpgradePanel>(FindObjectsInactive.Include);}
        if (selectionManager == null)
        {
        selectionManager = FindFirstObjectByType<TowerSelectionManager>();
        }


    }

    private void Awake()
    {
        if (mainCamera == null) mainCamera = Camera.main;
       
    }

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        ogColor = sr.color;
        if (upgradePanel == null) {upgradePanel = FindFirstObjectByType<TowerUpgradePanel>(FindObjectsInactive.Include);}
        if (selectionManager == null) {selectionManager = FindFirstObjectByType<TowerSelectionManager>();}
    }

    private void Highlight() => sr.color = highlightColor;
    public void Deselect()
    {
      initialization();

        sr.color = ogColor;
        upgradePanel.Hide();
    }

    void Update()
    {
         initialization();
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (selectionManager == null)
            {
                Debug.LogWarning($"{name}: selectionManager is null in Update()");
                return;
            }

            
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, selectionManager.towerLayer);

            if (hit.collider != null && hit.collider.transform.root == transform)
            {
                Highlight();
                TowerUpgradeComponent towerComp = GetComponent<TowerUpgradeComponent>();

                if (towerComp == null)
                { Debug.LogWarning($"{name}: TowerUpgradeComponent not found on {gameObject.name} or its expected location."); }
                if (towerComp != null)
                {
                    upgradePanel.Show(towerComp);
                    selectionManager.RegisterSelectedTower(this);
                }
            }
        }
    }
}
