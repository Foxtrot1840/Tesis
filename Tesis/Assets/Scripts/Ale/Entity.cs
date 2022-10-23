using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable
{
    protected int currentHealth;
    public GameObject damageParticles;

    public virtual void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void GetDamage(int damage, Vector3 point, Vector3 normal)
    {
        if (damageParticles != null)
        {
            var fireworks = Instantiate(damageParticles);
            fireworks.transform.position = point;
            fireworks.transform.rotation = Quaternion.Euler(normal);
            Destroy(fireworks,5f);
            currentHealth -= damage;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
