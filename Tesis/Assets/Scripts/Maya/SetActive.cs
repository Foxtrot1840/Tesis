using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            CanvasManager.instance.ActivePuzzle(true);
            Destroy(gameObject);
        }
    }
}
