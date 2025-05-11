using UnityEngine;

public class BuildTile : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;

    public bool CanBuildHere() => !isOccupied; //return true only if the tile has no turret on it

   public void Build(GameObject turretPrefab)
    {
    if (!isOccupied)
    {
        GameObject turret = Instantiate(turretPrefab, transform.position, Quaternion.identity);
        isOccupied = true;

        Debug.Log("Building turret on tile: " + gameObject.name);

        TowerClickDetector clickDetector = turret.GetComponent<TowerClickDetector>();
        if (clickDetector != null)
        {
            var panel = FindObjectOfType<TowerUpgradePanel>(true);
            var manager = FindObjectOfType<TowerSelectionManager>(true);
            clickDetector.upgradePanel = FindObjectOfType<TowerUpgradePanel>();
            clickDetector.selectionManager = FindObjectOfType<TowerSelectionManager>();
            Debug.Log($"[Assign] upgradePanel: {(panel != null ? "OK" : "NULL")}, selectionManager: {(manager != null ? "OK" : "NULL")}");
        }
        else
        {
            Debug.LogWarning($"{turret.name}: TowerClickDetector not found on turret prefab.");
        }
    }
}

}
