using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeUIHandler : MonoBehaviour
{
    [HideInInspector] public TowerPerk perk;
    [HideInInspector] public TowerUpgradeComponent tower;

    [SerializeField] private Image defaultIcon;
    [SerializeField] private Button button;
    [SerializeField] private Text perkNameText;      
    [SerializeField] private Text perkStatText;     


    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
        UpdateVisuals(); // Optional
    }

    private void OnClick()
    {
        if (tower != null && perk != null)
        {
            tower.ApplyPerkFromUI(perk);
        }
    }

    public void UpdateVisuals()
    {
        if (perkNameText != null)
            perkNameText.text = perk.perkName;
        if(perk.icon!= null){defaultIcon.sprite = perk.icon;}

        if (perkStatText != null)
        {
            perkStatText.text = $"DMG x{perk.damageModifier}, " +
                                $"FR x{perk.fireRateModifier}, " +
                                $"RNG x{perk.rangeModifier}";
        }
    }
}
