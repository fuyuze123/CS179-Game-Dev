using UnityEngine;

public class TowerUpgradePanel : MonoBehaviour
{
    public GameObject perkButtonPrefab;
    private CanvasGroup canvasGroup;
    public Transform pathAContainer;
    public Transform pathBContainer;
    public TowerSelectionManager selectionManager; // Assign in inspector or auto-find
    private TowerUpgradeComponent currentTower;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (selectionManager == null)
        {
            selectionManager = FindObjectOfType<TowerSelectionManager>();
        }
    }




    public void Show(TowerUpgradeComponent tower)
    {
        currentTower = tower;
        ClearButtons();

        PopulatePath(tower.pathA.firstPerk, pathAContainer);
        PopulatePath(tower.pathB.firstPerk, pathBContainer);

        gameObject.SetActive(true);
        if(canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        }

        
        transform.position = tower.transform.position + new Vector3(0, 1.5f, 0);
    }

    private void PopulatePath(TowerPerk startPerk, Transform container)
    {
        TowerPerk current = startPerk;
        Debug.Log("Created button for: " + current.perkName);

        while (current != null)
        {
            GameObject buttonObj = Instantiate(perkButtonPrefab, container);
            TowerUpgradeUIHandler handler = buttonObj.GetComponent<TowerUpgradeUIHandler>();
            handler.perk = current;
            handler.tower = currentTower;
            handler.UpdateVisuals(); // optional
            current = current.nextPerk;
        }
    }

    private void ClearButtons()
    {
        foreach (Transform child in pathAContainer) Destroy(child.gameObject);
        foreach (Transform child in pathBContainer) Destroy(child.gameObject);
    }


    public void OnSellButtonClicked()
    {
        if(selectionManager !=null){selectionManager.RegisterSelectedTower(null);}
        if (currentTower != null)
        {
        // Refund gold? Let's do 45 for now. 
        GoldRewarder.instance.ChangeGold(+45);


        Destroy(currentTower.gameObject);

        // Clear and hide panel
        currentTower = null;
        Hide();
        }



    }

    public void Hide()
    {

        if (canvasGroup != null)
        {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        }
        gameObject.SetActive(false);



    }
}
