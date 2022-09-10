using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Transform _grappHook;
    [SerializeField] Transform _handPos;
    [SerializeField] Transform _playerBody;
    [SerializeField] float _maxDistance;
    [SerializeField] float _hookSpeed;
    [SerializeField] Vector3 _offset;
    [SerializeField] LineRenderer _line;

    private bool _isGrapping, _isShooting;
    private Vector3 _hookPoint;
    private Player _player;

    private void Start()
    {
        _isShooting = false;
        _isGrapping = false;
        _player = _playerBody.GetComponent<Player>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _player.isZoom)
        {
            ShootHook();
        }

        if (_isGrapping)
        {
            _grappHook.position = Vector3.Lerp(_grappHook.position, _hookPoint, _hookSpeed * Time.deltaTime);
            if(Vector3.Distance(_grappHook.position, _hookPoint) < 0.5f)
            {
                _playerBody.position = Vector3.Lerp(_playerBody.position, _hookPoint - _offset, _hookSpeed * Time.deltaTime);
                _playerBody.GetComponent<Rigidbody>().useGravity = false;

                if(Vector3.Distance(_playerBody.position, _hookPoint - _offset) < 0.5f)
                {
                    _line.enabled = false;
                    _isGrapping = false;
                    _grappHook.parent = _handPos;
                    _playerBody.GetComponent<Rigidbody>().useGravity = true;
                    _grappHook.localPosition = Vector3.zero;
                }
            }
        }
    }

    private void LateUpdate()
    {
        _line.SetPosition(0, _handPos.position);
        _line.SetPosition(1, _hookPoint);
    }

    void ShootHook()
    {
        if (_isGrapping || _isShooting) return;

        _isShooting = true;
        if (Physics.Raycast(_handPos.position, _camera.transform.forward, out RaycastHit hitInfo, _maxDistance))
        {
            _line.enabled = true;
            _hookPoint = hitInfo.point;
            _isGrapping = true;
            _grappHook.parent = null;
            _grappHook.LookAt(_hookPoint);
            Debug.DrawLine(_handPos.position, _hookPoint);
        }
        _isShooting = false;
    }
}
