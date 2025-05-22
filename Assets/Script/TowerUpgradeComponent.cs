using UnityEngine;
using System.Collections.Generic;

public class TowerUpgradeComponent : MonoBehaviour
{
    public TowerUpgradePath pathA;
    public TowerUpgradePath pathB;
    public TowerPerkRegistry perkRegistry;

    private TowerUpgradePath selectedPath;
    private TowerPerk currentPerk;

    public void SelectUpgradePath(bool isPathA)
    {
        if (selectedPath != null) {return;} 

        selectedPath = isPathA ? pathA : pathB;
        currentPerk = selectedPath.firstPerk;
        ApplyPerk(currentPerk);
    }

    public void UpgradeToNextPerk()
    {
        if (currentPerk != null && currentPerk.nextPerk != null)
        {
            currentPerk = currentPerk.nextPerk;
            ApplyPerk(currentPerk);
        }
    }

    private void ApplyPerk(TowerPerk perk)
    {
        GetComponent<Tower>().updateDamage(perk.damageModifier);
        GetComponent<Tower>().updateFireRate(perk.fireRateModifier);
        GetComponent<Tower>().updateRange(perk.rangeModifier);
        Debug.Log("Upgraded");
    }


        public bool CanSelect(TowerPerk perk)
    {
        if (perk == null) {return false;}

        // No path selected — only allow first perk in either path
        if (selectedPath == null)
        {
            return (pathA != null && pathA.firstPerk == perk)
                || (pathB != null && pathB.firstPerk == perk);
        }

        if (!selectedPath.Contains(perk)) return false;

        // If no currentPerk yet (shouldn’t happen anymore), allow the first one
        if (currentPerk == null)
        {
            return perk == selectedPath.firstPerk;
        }

        // Allow the next perk only
        return perk == currentPerk.nextPerk;
    }



    public void ApplyPerkFromUI(TowerPerk perk)
    {
        if (perk == null) return;

        if (selectedPath == null)
        {
            if (pathA != null && pathA.firstPerk == perk)
                selectedPath = pathA;
            else if (pathB != null && pathB.firstPerk == perk)
                selectedPath = pathB;
            else
            {
                Debug.LogWarning($"[TowerUpgradeComponent] Invalid first perk: {perk.name}");
                return;
            }
        }

        if (!CanSelect(perk)) return;

        // Check for gold
        if (GoldRewarder.instance.GetCurrentGold() < perk.upgradeCost)
        {
            Debug.Log("Not enough gold to apply upgrade.");
            return;
        }

        // Deduct gold
        GoldRewarder.instance.ChangeGold(-perk.upgradeCost);

        currentPerk = perk;
        ApplyPerk(perk);
        }

    public string GetCurrentPerkName()
    {
        return currentPerk != null ? currentPerk.perkName : "";
    }

    public List<string> GetAppliedPerkNames()
    {
        List<string> names = new List<string>();
        TowerPerk p = selectedPath?.firstPerk;

        while (p != null && currentPerk != null)
        {
            names.Add(p.perkName);
            if (p == currentPerk) break;
            p = p.nextPerk;
        }

        return names;
    }


    public void ApplyPerksByNames(List<string> perkNames)
    {
        if (perkRegistry == null)
        {
            Debug.LogError("Perk Registry is not assigned.");
            return;
        }

        foreach (string perkName in perkNames)
        {
            TowerPerk perk = perkRegistry.GetPerkByName(perkName);
            if (perk == null)
            {
                Debug.LogWarning($"Perk '{perkName}' not found in registry.");
                continue;
            }

            if (selectedPath == null)
            {
                if (pathA != null && pathA.firstPerk == perk)
                    selectedPath = pathA;
                else if (pathB != null && pathB.firstPerk == perk)
                    selectedPath = pathB;
            }

            if (!CanSelect(perk))
                break;

            currentPerk = perk;
            ApplyPerk(perk);
        }
    }

    private TowerPerk FindPerkByName(string name)
    {
        return perkRegistry.GetPerkByName(name);
    }

}
