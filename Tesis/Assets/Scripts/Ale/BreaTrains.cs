using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreaTrains : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameManager.instance.player)
        {
            collision.gameObject.transform.position =
                GameManager.instance.player.GetComponent<Controller>().lastSavePoint;
        }
    }
}
