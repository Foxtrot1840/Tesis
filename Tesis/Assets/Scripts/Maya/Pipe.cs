using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    float[] rotations = { 0, 90, 270 };

    public float[] correctRotation;

    [SerializeField]
    bool isPlaced = false;

    int possibleRots = 1;

    GameManagerPipe gameManagerPipe;

    private void Awake()
    {
        gameManagerPipe = GameObject.Find("GameManagerPipe").GetComponent<GameManagerPipe>();
    }

    private void Start()
    {
        possibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);

        if (possibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isPlaced = true;
                gameManagerPipe.CorrectMove();
            }
        }
        else 
        {
            Debug.Log(name);
            Debug.Log(correctRotation);
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isPlaced = true;
                gameManagerPipe.CorrectMove();
            }
        }
    }

    public void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, -90));

        if (possibleRots > 1)
        {
            if (transform.eulerAngles.z == correctRotation[0] || transform.eulerAngles.z == correctRotation[1])
            {
                isPlaced = true;
                gameManagerPipe.CorrectMove();
            }
            else if (isPlaced == true)
            {
                isPlaced = false;
                gameManagerPipe.WrongMove();
            }
        }
        else
        {
            if (transform.eulerAngles.z == correctRotation[0])
            {
                isPlaced = true;
                gameManagerPipe.CorrectMove();
            }
            else if(isPlaced == true)
            {
                isPlaced = false;
                gameManagerPipe.WrongMove();
            }
        }
    }
}
