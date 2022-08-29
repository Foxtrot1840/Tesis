using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
