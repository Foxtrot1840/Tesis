using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spider;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float cooldown = 5;
    private bool _isActive = false;
    private int activeGears = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != GameManager.instance.player) return;
        _isActive = true;
        StartCoroutine(Spawn());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != GameManager.instance.player) return;
        _isActive = false;
    }

    IEnumerator Spawn()
    {
        while (_isActive == true)
        {
            yield return new WaitForSeconds(cooldown);
            Instantiate(spider, spawnPoint.position, spider.transform.rotation).GetComponent<Spider>()._range = 100;
        }
    }

    public void DesactiveGear()
    {
        activeGears--;
        if(activeGears <= 0)Destroy(this);
    }
}
