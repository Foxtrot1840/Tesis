using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPoint : MonoBehaviour
{
    public enum HookMovements{Normal, Swing}

    public HookMovements movement;
    public GameObject message;

    private void Start()
    {
        message.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            other.GetComponent<Controller>().hookSwingPoint.Add(transform);
            message.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            other.GetComponent<Controller>().hookSwingPoint.Remove(transform);
            message.SetActive(false);
        }
    }
}
