using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class GoldRewarder : MonoBehaviour
{
    public static GoldRewarder instance;
    
    [Header("Events")]
    public static UnityEvent<int> onGoldChange = new UnityEvent<int>();

    private int current_amount; //current amount of gold player has
    private int base_amount = 100;

    private void Awake() //purpose of this is so there is no multiple occurances of goldRewarder
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        current_amount = base_amount;
        onGoldChange.Invoke(current_amount);
    }

    public void ChangeGold(int amount)
    {
        current_amount += amount;
        Debug.Log("Gold Added: " + amount);
        // Debug.Log("Current Gold: " + current_amount);
        onGoldChange.Invoke(current_amount);
    }

    public int GetCurrentGold()
    {
        return current_amount;
    }
}