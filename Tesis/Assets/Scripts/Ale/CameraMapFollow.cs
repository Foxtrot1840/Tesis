using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraMapFollow : MonoBehaviour
{
    private Transform _player;
    
    private void Start()
    {
        _player = GameManager.instance.player.transform;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = _player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        
        //transform.rotation = quaternion.Euler(90f,_player.rotation.y,0f);
    }
}
