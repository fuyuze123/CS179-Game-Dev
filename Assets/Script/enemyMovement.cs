using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private bool isDestroyed = false;

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        target = LevelManagingScript.main.path[0]; //first element
    }

    private void Update()
    {
        if(Vector2.Distance(target.position,transform.position) <= 0.1f)
        {
        pathIndex++;
       
            if(pathIndex == LevelManagingScript.main.path.Length)
            {
                LevelManagingScript.main.DealDamage();
                LevelManagingScript.main.PrintHealth();
                EnemySpawner.onEnemyDestroy.Invoke();
                isDestroyed = true;
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManagingScript.main.path[pathIndex];
                LevelManagingScript.main.PrintHealth();
            }
        }

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
}
