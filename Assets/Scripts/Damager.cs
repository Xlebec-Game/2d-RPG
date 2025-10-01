using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    private float _damage;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(_damage);
        }

        if (collision.TryGetComponent(out Boss boss))
        {
            boss.TakeDamage(_damage);
        }
    }
}
