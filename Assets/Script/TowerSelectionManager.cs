using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSelectionManager : MonoBehaviour
{
    [SerializeField] public LayerMask towerLayer;
    public TowerClickDetector selectedTower;

    public void RegisterSelectedTower(TowerClickDetector tower)
    {
        if (selectedTower != null && selectedTower != tower)
        {
            selectedTower.Deselect();
        }

        selectedTower = tower;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, towerLayer);

            if (hit.collider == null && selectedTower != null)
            {
                selectedTower.Deselect();
                selectedTower = null;
            }
        }
    }
}
