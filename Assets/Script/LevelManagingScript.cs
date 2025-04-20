using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelManagingScript : MonoBehaviour
{
    public static LevelManagingScript main;
    public Transform startPoint;
    public Transform[] path;

    [SerializeField] private PlayerHealth playerHealth;

    private void Awake()
    {
        main = this;
        if (playerHealth == null)
        {
            Debug.Log("PlayerHealth is null");
        }
    }

    public void DealDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }

    public void PrintHealth()
    {
        if (playerHealth != null)
        {
            Debug.Log("Player Health: " + playerHealth.GetHealth());
        }
    }
}
