using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelManagingScript : MonoBehaviour
{
    public static LevelManagingScript main;
    public Transform startPoint;
    public Transform[] path;

    private void Awake()
    {
        main = this;
    }

    public void DealDamage()
    {
        if (PlayerHealth.instance != null)
        {
            PlayerHealth.instance.TakeDamage(1);
        }
    }

    public void PrintHealth()
    {
        if (PlayerHealth.instance != null)
        {
            Debug.Log("Player Health: " + PlayerHealth.instance.GetHealth());
        }
    }
}
