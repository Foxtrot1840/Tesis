using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{
    private GameObject _player;
    private void Start()
    {
        _player = GameManager.instance.player;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == _player) EventManager.TriggerEvent(EventManager.EventsType.Event_FinishGame, true);
    }
}
