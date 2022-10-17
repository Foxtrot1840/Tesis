using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Interactuables : MonoBehaviour
{
    protected GameObject player;
    protected Controller plyController;
    
    protected virtual void Start()
    {
        player = GameManager.instance._player;
        plyController = player.GetComponent<Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        Debug.Log(player);
        if(other.gameObject == player)
        {
            Debug.Log("A");
            plyController.interactables += Action;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("C");
            plyController.interactables -= Action;
        }
    }

    protected abstract void Action();

    private void OnDisable()
    {
        plyController.interactables -= Action;
    }
}