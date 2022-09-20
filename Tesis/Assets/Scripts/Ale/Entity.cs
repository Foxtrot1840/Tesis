using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamagable
{
    public int currentHealth;

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public  virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
