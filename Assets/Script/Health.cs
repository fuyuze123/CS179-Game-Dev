using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int HP = 2;

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
