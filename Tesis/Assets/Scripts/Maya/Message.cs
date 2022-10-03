using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField]
    private GameObject objectMessage;

    private bool activeMessage;

    private void Update()
    {
        if (activeMessage == true)
        {
            objectMessage.SetActive(false);
        }
    }
}
