using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool _destroyObject = false;
    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable == null) return;
        damagable.GetDamage(damage);
        if(_destroyObject) DestroyImmediate(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        if (damagable == null) return;
        damagable.GetDamage(damage);
        if(_destroyObject) DestroyImmediate(gameObject);
    }
}