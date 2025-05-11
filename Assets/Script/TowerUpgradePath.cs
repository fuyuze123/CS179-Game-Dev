using UnityEngine;

[CreateAssetMenu(menuName = "Tower/UpgradePath")]
public class TowerUpgradePath : ScriptableObject
{
    public string pathName;
    public TowerPerk firstPerk;


    public bool Contains(TowerPerk perk)
    {
        TowerPerk current = firstPerk;
        while (current != null)
        {
            if (current == perk) return true;
            current = current.nextPerk;
        }
        return false;
    }




}


