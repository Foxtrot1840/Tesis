using System.Collections;
using System.Collections.Generic;
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

    public virtual void GetDamage(int damage, Vector3 particles)
    {
        var fireworks = Instantiate(damageParticles);
        fireworks.transform.position = particles;
        Destroy(fireworks,5f);
        currentHealth -= damage;
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
