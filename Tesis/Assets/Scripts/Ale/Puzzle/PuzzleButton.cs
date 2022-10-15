using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : Interactuables
{
    public List<PuzzleCircle> circles;
    private Renderer _renderer;

    protected override void Start()
    {
        base.Start();
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.white;
    }

    protected override void Action()
    {
        Debug.Log("B");
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
