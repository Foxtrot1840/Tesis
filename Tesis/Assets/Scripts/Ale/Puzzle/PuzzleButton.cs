using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public List<PuzzleCircle> circles;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.white;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != GameManager.instance._player) return;

        StartCoroutine(ChangeColor());
        
        foreach (var circle  in circles)
        {
            circle.ChangeState();
        }
    }

    IEnumerator ChangeColor()
    {
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _renderer.material.color = Color.white;
    }
}
