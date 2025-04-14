using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attribute")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private void start()
    {
        target = LevelManagingScript.main.path[0]; //first element
    }
    private void Update()
    {
        if(Vector2.Distance(target.position,transform.position) <= 0.1f)
        {
        pathIndex++;
       
            if(pathIndex == LevelManagingScript.main.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManagingScript.main.path[pathIndex];
            }
        }

    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }
}
