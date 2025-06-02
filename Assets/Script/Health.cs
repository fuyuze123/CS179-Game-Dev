using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int maxHealth = 2;
    private int currentHealth;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        // Let another script handle visuals and destruction
        EnemyMovement movement = GetComponent<EnemyMovement>();
        if (movement != null)
        {
            movement.TriggerDeathSequence();
            
        }
    }


        public int GetCurrentHealth()
    {
        return Mathf.Max(0, currentHealth);
    }
}
