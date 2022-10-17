using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleCircle[] circles = new PuzzleCircle[5];
    public GameObject[] buttons;
    [SerializeField] private GameObject bridge;
    
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
                bridge.SetActive(true);

                foreach (var circle in circles)
                {
                    circle._onUpdate += circle.RotateToFinal;
                }

                foreach (var button in buttons)
                {
                    button.SetActive(false);
                }
            }
        }
    }
}
