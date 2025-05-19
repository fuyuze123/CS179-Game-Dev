using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Tower : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f; //
    [SerializeField] private float rotationSpeed = 5f; // Speed of turret rotation
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private int damageModifier = 1;

    private Transform target;  // The target to be tracked
    private float timeUntilNextBullet;

    public void updateFireRate(float newfire){fireRate *= newfire;}
    public void updateDamage(int newDamage){damageModifier *= newDamage;}
    public void updateRange(float newRange){targetingRange *= newRange;}

    private void FindTarget()
    {
        RaycastHit2D[] hits =  Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2) transform.position, 0f, enemyMask);

        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void rotateTowardTarget()
    {
        Vector2 direction = target.position - turretRotationPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation  = Quaternion.Euler(new Vector3(0, 0, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool IsValidTarget(Transform target)
    {
    if (target == null) return false;

    Health health = target.GetComponent<Health>();
    if (health == null) return false;

    return health.GetCurrentHealth() > 0;
    }


    private void Update()
    {
        if (target == null || !IsValidTarget(target))
        {
            FindTarget();
            return;
        }

        rotateTowardTarget();
        if (Vector2.Distance(turretRotationPoint.position, target.position) > targetingRange)
        {
            target = null;
            return;        
            
            }
        else
        {
            timeUntilNextBullet += Time.deltaTime;
            if(timeUntilNextBullet >= 1f / fireRate)
            {
                shoot();
                timeUntilNextBullet = 0f;
            }
        }

    }
    private void shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.damageMultiplier(damageModifier);
        bulletScript.SetTarget(target);
    }
    







    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(turretRotationPoint.position, targetingRange);
    }








}
