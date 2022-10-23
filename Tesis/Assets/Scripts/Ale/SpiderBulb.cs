using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiderBulb : Entity
{
    private void Start()
    {
        currentHealth = 1;
    }

    public override void GetDamage(int damage, Vector3 point, Vector3 normal)
    {
        Die();
    }

    public override void Die()
    {
        Debug.Log("B");
       gameObject.GetComponentInParent<Entity>().Die();
    }
}
