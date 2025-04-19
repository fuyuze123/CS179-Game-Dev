using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelManagingScript : MonoBehaviour
{
    public static LevelManagingScript main;
    public Transform startPoint;
    public Transform[] path;
    public int playerHealth = 100;

    private void Awake()
    {
        main = this;
    }
    public void DealDamage()
    {
        playerHealth--;
        Debug.Log("Damage Dealt!");
    }
    public void PrintHealth()
    {
        Debug.Log("Player Health: " + playerHealth);
    }
}
