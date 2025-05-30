using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isBoss = false;
    private Health healthComponent;


    [Header("Attribute")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private bool isDestroyed = false;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        if (LevelManagingScript.main.path != null)
        {
            target = LevelManagingScript.main.path[0];
        }

        healthComponent = GetComponent<Health>();
        if (healthComponent == null)
        {
            Debug.LogError("Error with health component!");
            return;
        }
    }

    private void Update()
    {
        if(Vector2.Distance(target.position,transform.position) <= 0.1f)
        {
        pathIndex++;
       
            if (pathIndex == LevelManagingScript.main.path.Length)
            {
                LevelManagingScript.main.DealDamage(healthComponent.GetCurrentHealth());

                if (isBoss)
                {
                    GoldRewarder.instance.ChangeGold(200);
                }

                EnemySpawner.onEnemyDestroy.Invoke();
                isDestroyed = true;
                Destroy(gameObject);
                return;
            }
            else
            {
                if (pathIndex < LevelManagingScript.main.path.Length)
                {
                    target = LevelManagingScript.main.path[pathIndex];
                }
            }
        }

    }

    public void TriggerDeathSequence()
    {
        if (isDestroyed) return;
        if (UIManager.instance != null){UIManager.instance.UpdateEnemyDefeatedUI();}

        StartCoroutine(dyingSequence());
    }


    public IEnumerator dyingSequence()
    {
      
       isDestroyed = true;
       rb.linearVelocity = Vector2.zero;
       animator.SetTrigger("Die");

            
       yield return new WaitForSeconds(1.2f);

       if (isBoss)
       {
        GoldRewarder.instance.ChangeGold(200);
        }

        EnemySpawner.onEnemyDestroy.Invoke();
        Destroy(gameObject);
        
    }

    private void FixedUpdate()
    {
        if (isDestroyed || target == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        WallHealth wallHealth = collision.gameObject.GetComponent<WallHealth>();
        if (wallHealth != null && healthComponent != null)
        {
            int wallOriginalHealth = wallHealth.GetCurrentHealth();
            int enemyCurrentHealth = healthComponent.GetCurrentHealth();

            if (enemyCurrentHealth > 0)
            {
                wallHealth.TakeDamage(enemyCurrentHealth);
                if (wallOriginalHealth > 0)
                {
                    healthComponent.TakeDamage(wallOriginalHealth);
                    // Check if enemy is dead after wall hit
                    if (healthComponent.GetCurrentHealth() <= 0)
                    {
                        if (isBoss)
                        {
                          GoldRewarder.instance.ChangeGold(200); // reward gold;
                        }
                        
                    }
                }
            }
        }
    }
}
