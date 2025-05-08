using UnityEngine;

public class WallHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        Destroy(gameObject); // Wall destroyed
    }

    public int GetCurrentHealth()
    {
        return Mathf.Max(0, currentHealth);
    }
}
