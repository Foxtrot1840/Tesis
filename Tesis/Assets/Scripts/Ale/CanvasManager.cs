using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;
    [SerializeField] private GameObject _pipePuzzle;
    [SerializeField] private GameObject _pausePanel;

    private bool _pauseActive;

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
        _pausePanel.SetActive(false);
        _pauseActive = false;
    }

    public void ActivePuzzle(bool active)
    {
        Time.timeScale = active ? 0 : 1;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = active;
        _pipePuzzle.SetActive(active);
    }
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.P)) return;
        
        PauseMenu();
    }

    public void PauseMenu()
    {
        _pauseActive = !_pauseActive;
        Time.timeScale = _pauseActive ? 0 : 1;
        Cursor.visible = _pauseActive;
        Cursor.lockState = _pauseActive ? CursorLockMode.None : CursorLockMode.Locked;
        _pausePanel.SetActive(_pauseActive);
    }
    
    private void FinishLevel(params object[] p)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _victoryScreen.SetActive((bool)p[0]);
        _defeatScreen.SetActive(!(bool)p[0]);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
