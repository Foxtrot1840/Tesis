using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreaTrains : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == GameManager.instance._player)
        {
            collision.gameObject.transform.position =
                GameManager.instance._player.GetComponent<Controller>().lastSavePoint;
        }
    }
}
