using UnityEngine;

public class GoldRewarder : MonoBehaviour
{
    public static GoldRewarder instance; 
    public int current_amount = 0; //current amount of gold player has
    public delegate void goldChanged(int newGold);
    public event goldChanged goldChanged;

    private void Awake() //purpose of this is so there is no multiple occurances of goldRewarder
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void increaseGold(int amount) {
        current_amount += amount;

        if (goldChanged != null) {
            goldChanged.Invoke(current_amount);
        }
    }

    
    
}