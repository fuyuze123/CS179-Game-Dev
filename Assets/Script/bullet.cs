using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class bullet : MonoBehaviour
{
    private Transform target;

    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletVelocity = 10f;
    [SerializeField] private int bulletDamage = 1;


    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector2 direction = target.position - transform.position;
        direction = direction.normalized;
        rb.linearVelocity = direction * bulletVelocity;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health enemyHealth = collision.gameObject.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
        
    }
}
