using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NewMonoBehaviourScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f; //


    private Transform target;  // The target to be tracked
    
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
        turretRotationPoint.rotation = targetRotation;
    }


    private void Update()
    {
        if (target == null)
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

    }
    







    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(turretRotationPoint.position, targetingRange);
    }








}
