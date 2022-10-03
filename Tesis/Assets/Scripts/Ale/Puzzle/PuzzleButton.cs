using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public List<PuzzleCircle> circles;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != GameManager.instance._player) return;
        
        foreach (var circle  in circles)
        {
            circle.ChangeState();
        }
    }
}
