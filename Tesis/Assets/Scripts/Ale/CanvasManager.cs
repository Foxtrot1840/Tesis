using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _pipePuzzle;

    public static CanvasManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        EventManager.Subscribe(EventManager.EventsType.Event_FinishGame,FinishLevel);
    }

    private void Start()
    {
        _victoryScreen.SetActive(false);
        _defeatScreen.SetActive(false);
        _pipePuzzle.SetActive(false);
    }

    public void ActivePuzzle(bool active)
    {
        Time.timeScale = active ? 0 : 1;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = active;
        _pipePuzzle.SetActive(active);
    }

    private void FinishLevel(params object[] p)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _victoryScreen.SetActive((bool)p[0]);
        _defeatScreen.SetActive(!(bool)p[0]);
    }
}
