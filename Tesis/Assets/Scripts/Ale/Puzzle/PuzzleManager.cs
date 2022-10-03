using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.TerrainAPI;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleCircle[] circles = new PuzzleCircle[5];

    private void Start()
    {
        foreach (var circle in circles)
        {
            circle.manager = this;
        }
    }

    public void Check()
    {
        if (circles.All(x => x._activate == false))
        {
            var angle = circles[0].transform.forward;
            var cont = 0;
            foreach (var circle in circles)
            {
                if (circle.transform.forward == angle)
                {
                    Debug.Log("uno bien");
                    cont++;
                }
                else
                {
                    Debug.Log("uno mal");
                }
            }
            if (cont == circles.Length)
            {
                //Activar algo
            }
        }
    }
}
