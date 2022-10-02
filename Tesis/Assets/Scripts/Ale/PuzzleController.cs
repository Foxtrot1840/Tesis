using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private void Update()
    {
        //transform.rotation = new Quaternion.Euler(0,transform.rotation.y + Time.deltaTime * speed,0);
    }
}
