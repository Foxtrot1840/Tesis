using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            other.GetComponent<Controller>().hookSwingPoint.Add(transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            other.GetComponent<Controller>().hookSwingPoint.Remove(transform.position);
        }
    }
}
