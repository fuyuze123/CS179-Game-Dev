using UnityEngine;

public class TowerUpgradeComponent : MonoBehaviour
{
    public TowerUpgradePath pathA;
    public TowerUpgradePath pathB;

    private TowerUpgradePath selectedPath;
    private TowerPerk currentPerk;

    public void SelectUpgradePath(bool isPathA)
    {
        if (selectedPath != null) return;  

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
    }


    public bool CanSelect(TowerPerk perk)
{
    if (selectedPath == null)
    {
        // Choosing first perk from either path
        if (pathA.Contains(perk) && pathA !=null)
        {
            selectedPath = pathA;
            return perk == pathA.firstPerk;
        }
        else if (pathB.Contains(perk)&& pathB !=null)
        {
            selectedPath = pathB;
            return perk == pathB.firstPerk;
        }
        return false;
    }
    else
    {
        // Only allow continuing down the selected path
        if (!selectedPath.Contains(perk)) return false;
        return perk == currentPerk.nextPerk;
    }
}

    public void ApplyPerkFromUI(TowerPerk perk)
    {
        if (!CanSelect(perk)) return;

         currentPerk = perk;
         ApplyPerk(perk);
    }

}
