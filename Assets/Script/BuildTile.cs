using UnityEngine;

public class BuildTile : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;

    public bool CanBuildHere() => !isOccupied; //return true only if the tile has no turret on it

    public void Build(GameObject turretPrefab)
    {
        if (!isOccupied)
        {
            Instantiate(turretPrefab, transform.position, Quaternion.identity);
            isOccupied = true;
        }
    }
}
