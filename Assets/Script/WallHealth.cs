using UnityEngine;

public class WallHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Wall destroyed
        }
    }
}
