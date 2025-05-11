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

    private void Awake()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
    }

    private void Start()
    {
        if (button == null) {button = GetComponent<Button>();}


        button.onClick.AddListener(OnClick);
        GoldRewarder.onGoldChange.AddListener(OnGoldChanged);

        UpdateVisuals();
    }

    private void OnDestroy()
    {
        GoldRewarder.onGoldChange.RemoveListener(OnGoldChanged);
    }   

    private void OnGoldChanged(int newGold)//This function calls itself when the goldreward version of goldchange gets call
    {
    UpdateVisuals();
    }

    private void OnClick()
    {
        if (tower != null && perk != null)
        {
            tower.ApplyPerkFromUI(perk);

        TowerUpgradePanel panel = FindFirstObjectByType<TowerUpgradePanel>();
        if (panel != null)
        {
            panel.Show(tower); // refresh
        }


        }
    }

        public void UpdateVisuals()
        {
            if (perkNameText != null)
                {perkNameText.text = perk != null ? perk.perkName : "[null perk]";}

            if (perkStatText != null && perk != null)
            {
                perkStatText.text = $"DMG x{perk.damageModifier}, " +
                                    $"FR x{perk.fireRateModifier}, " +
                                    $"RNG x{perk.rangeModifier}";
            }

            if (defaultIcon != null && perk != null && perk.icon != null)
            {
                defaultIcon.sprite = perk.icon;
            }

          
            if (tower == null || perk == null)
            {
                Debug.LogWarning($"[UIHandler] Tower or Perk is null! tower={tower}, perk={perk}");
                button.interactable = false;
                return;
            }

            bool canAfford = GoldRewarder.instance.GetCurrentGold() >= perk.upgradeCost;
            bool canSelect = tower != null && tower.CanSelect(perk);

            if (!canSelect || !canAfford)
            {
                button.interactable = false;
                var colors = button.colors;
                colors.normalColor = Color.gray;
                button.colors = colors;
            }
            else
            {
                button.interactable = true;
                var colors = button.colors;
                colors.normalColor = Color.white;
                button.colors = colors;
            }

        }

}
