using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using Unity.Mathematics;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    private Transform[] spawners;
    [SerializeField] private GameObject spider;

    private void Awake()
    {
        spawners = gameObject.GetComponentsInChildren<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != GameManager.instance.player) return;
        foreach (var spawn in spawners)
        {  
            GameObject newSpider = Instantiate(spider, spawn.transform.position, quaternion.identity);
            newSpider.GetComponent<Spider>()._range = 100;
        }
    }
}
