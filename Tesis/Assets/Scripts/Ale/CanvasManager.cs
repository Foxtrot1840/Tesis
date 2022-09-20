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

    private void Start()
    {
        _victoryScreen.SetActive(false);
        _defeatScreen.SetActive(false);
    }

    private void FinishLevel(params object[] p)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _victoryScreen.SetActive((bool)p[0]);
        _defeatScreen.SetActive(!(bool)p[0]);
    }
}
