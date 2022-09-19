using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
