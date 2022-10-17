using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerPipe : MonoBehaviour
{
    public GameObject pipesHolder;
    public GameObject[] pipes;

    [SerializeField]
    int totalPipes = 0;

    [SerializeField]
    int correctedPipes = 0;

    void Start()
    {
        totalPipes = pipesHolder.transform.childCount;

        pipes = new GameObject[totalPipes];

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i] = pipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectMove()
    {
        correctedPipes += 1;

        Debug.Log("Correct Move");

        if (correctedPipes == totalPipes)
        {
            Debug.Log("Ganaste");
        }
    }

    public void WrongMove()
    {
        correctedPipes -= 1;
    }
}
