using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;

    private void Awake()
    {
        EventManager.Subscribe(EventManager.EventsType.Event_FinishGame,FinishLevel);
    }

    private void FinishLevel(params object[] p)
    {
        Time.timeScale = 0;
        _victoryScreen.SetActive((bool)p[0]);
        _defeatScreen.SetActive(!(bool)p[0]);
    }
}
