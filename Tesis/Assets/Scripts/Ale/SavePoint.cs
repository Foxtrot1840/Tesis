using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
            other.GetComponent<Controller>().lastSavePoint = transform.position;
    }
}
