using UnityEngine;

public class TowerTileLink : MonoBehaviour
{
    private BuildTile originTile;

    public void SetOriginTile(BuildTile tile)
    {
        originTile = tile;
    }

    public void OnDestroy()
    {
        if (originTile != null)
        {
            originTile.SetUnoccupied();
        }
    }
}