using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Bullet : MonoBehaviour
{
    private Transform target;

    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletVelocity = 10f;
    [SerializeField] int bulletDamage = 1;

    [Header("Layers")]
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private LayerMask tileLayerMask;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    public void damageMultiplier(int modifier)
    {
        bulletDamage *= modifier;

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
        int hitLayerBit = 1 << collision.gameObject.layer;
        if ((enemyLayerMask.value & hitLayerBit) != 0)
        {
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bulletDamage);
            }
            Destroy(gameObject);
            return;
        }
        else if ((tileLayerMask.value & hitLayerBit) != 0)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            return;
        }
    }
}
