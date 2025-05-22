using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Tower/Perk Registry")]
public class TowerPerkRegistry : ScriptableObject
{
    public List<TowerPerk> allPerks;

    private Dictionary<string, TowerPerk> perkMap;

    public TowerPerk GetPerkByName(string name)
    {
        if (perkMap == null)
        {
            perkMap = new Dictionary<string, TowerPerk>();
            foreach (var perk in allPerks)
                perkMap[perk.perkName] = perk;
        }

        if (perkMap.TryGetValue(name, out var result))
            return result;

        Debug.LogWarning($"Perk '{name}' not found in registry.");
        return null;
    }
}
