using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Model
{
    private Rigidbody _rb;
    private GameObject _player;
    private float _speed;
    
    public Model(Rigidbody rb, GameObject player, float speed)
    {
        _rb = rb;
        _player = player;
        _speed = speed;
    }

    public void Move(Vector3 dir)
    {
        _rb.MovePosition(_player.transform.position + Vector3.Normalize(_player.transform.right * dir.x + _player.transform.forward * dir.z) * _speed * Time.fixedDeltaTime);
    }
}
