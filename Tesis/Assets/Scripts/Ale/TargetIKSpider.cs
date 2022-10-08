using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TargetIKSpider : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private float stepDistance;
    [SerializeField] private float _stepHeight;
    [SerializeField] private float _moveSpeed;
    [SerializeField]private TargetIKSpider _otherLeg;
    [SerializeField]private bool _canMove;
    private bool _isMoving;
    private float _lerp = 1;
    private Vector2 _offsetFoot;
    private Vector3 _oldPosition;
    private Vector3 _nextPosition;
    private Vector3 _currentPosition;

    public bool Movement
    {
        get { return _isMoving; }
        set { _canMove = value; }
    }
    
    private void Start()
    {
        _offsetFoot.x = transform.position.z - _body.position.z;
        _offsetFoot.y = transform.position.x - _body.position.x;
        _currentPosition = _oldPosition = _nextPosition = transform.position;
    }

    private void Update()
    {
        transform.position = _currentPosition;
        //Debug.DrawLine(_body.position - _body.right * _offsetFoot.y - _body.forward * _offsetFoot.x, transform.position,Color.magenta);
        if (_canMove && Physics.Raycast(_body.position - _body.right * _offsetFoot.y - _body.forward * (_offsetFoot.x + stepDistance/2) ,Vector3.down,out RaycastHit hit))
        {
            Debug.DrawLine(_body.position - _body.right * _offsetFoot.y - _body.forward * (_offsetFoot.x + stepDistance/2), hit.point,Color.blue);

            if (Vector3.Distance(_nextPosition, hit.point) >= stepDistance && _lerp >= 1)
            {
                _nextPosition = hit.point;
                transform.up = hit.normal;
                _lerp = 0;
                _isMoving = true;
            }
            else
            {
                if (_lerp < 1)
                {
                    Vector3 footPosition = Vector3.Lerp(_oldPosition, _nextPosition, _lerp);
                    footPosition.y += Mathf.Sin(_lerp * Mathf.PI) * _stepHeight;
                    _currentPosition = footPosition;
                    _lerp += Time.deltaTime * _moveSpeed;
                }
                else
                {
                    _oldPosition = _nextPosition;
                    if (_isMoving)
                    {
                        _isMoving = false;
                        _canMove = false;
                        _otherLeg.Movement = true;
                        Debug.Log("Cambio" + name);
                    }
                }
            }
        }
    }
}
