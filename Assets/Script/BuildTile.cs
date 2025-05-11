using UnityEngine;

public class BuildTile : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;
    private GameObject towerOnTile = null;

    public bool CanBuildHere() => (!isOccupied && towerOnTile == null); //return true only if the tile has no turret on it

    public void Build(GameObject turretPrefab)
    {
        if (CanBuildHere())
        {
            GameObject turret = Instantiate(turretPrefab, transform.position, Quaternion.identity);
            isOccupied = true;
            towerOnTile = turret;
            TowerTileLink tileLink = turret.GetComponent<TowerTileLink>();
            if (tileLink != null)
            {
                tileLink.SetOriginTile(this);
            }

            Debug.Log("Building turret on tile: " + gameObject.name);

            TowerClickDetector clickDetector = turret.GetComponent<TowerClickDetector>();
            if (clickDetector != null)
            {
                var panel = FindFirstObjectByType<TowerUpgradePanel>(FindObjectsInactive.Include);
                var manager = FindFirstObjectByType<TowerSelectionManager>(FindObjectsInactive.Include);
                clickDetector.upgradePanel = FindFirstObjectByType<TowerUpgradePanel>();
                clickDetector.selectionManager = FindFirstObjectByType<TowerSelectionManager>();
                Debug.Log($"[Assign] upgradePanel: {(panel != null ? "OK" : "NULL")}, selectionManager: {(manager != null ? "OK" : "NULL")}");
            }
            else
            {
                Debug.LogWarning($"{turret.name}: TowerClickDetector not found on turret prefab.");
            }
        }
    }

    public void SetUnoccupied()
    {
        isOccupied = false;
        towerOnTile = null;
    }
}
