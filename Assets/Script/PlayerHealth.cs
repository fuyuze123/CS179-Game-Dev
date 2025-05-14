using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [Header("Events")]
    public static UnityEvent<int> onPlayerHealthChange = new UnityEvent<int>();
    
    public int maxHealth = 100;
    public int currentHealth;

    private void Awake()
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
        currentHealth = maxHealth;
        onPlayerHealthChange.Invoke(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Damage Taken: " + damage); // Debug
        onPlayerHealthChange.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        GameManager.instance.GameOver();
        // Add game ending logic later
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
