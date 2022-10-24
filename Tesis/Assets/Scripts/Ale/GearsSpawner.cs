using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsSpawner : MonoBehaviour, IDamagable
{
    private void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    public void GetDamage(int damage)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.parent.GetComponent<SpiderSpawner>().DesactiveGear();
    }

    public void GetDamage(int damage, Vector3 point, Vector3 normal)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.parent.GetComponent<SpiderSpawner>().DesactiveGear();
    }
}
