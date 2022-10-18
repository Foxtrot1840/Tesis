using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField]
    private GameObject objectMessage;

    private void Start()
    {
        Invoke("TurnOff",3);
    }

    private void TurnOff()
    {
        objectMessage.SetActive(false);
    }
}
