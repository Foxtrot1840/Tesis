using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject _player;

    public GameObject player
    {
        get { return _player; }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        Time.timeScale = 1;
        EventManager.Subscribe(EventManager.EventsType.Event_FinishGame,FinishLevel);
    }

    public void FinishLevel(params object[] p)
    {   
        EventManager.ClearEvents();
        Time.timeScale = 0;
    }
}
