using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using TMPro;
using Unity.Mathematics;

public class Model
{
    private Rigidbody _rb;
    private Transform _player;
    private float _speedRotation;
    private float _speedAim;
    private float _jumpForce;
    private CinemachineTransposer _normalCameraOffset;
    private CinemachineTransposer _zoomCameraOffset;
    private Transform _hand;
    private Transform _hook;
    private float _maxDistanceHook;
    private Controller _controller;
    private LineRenderer _line;
    private float _currentSpeed;

    private bool _isGrapping, _going;
    private Vector3 _hookPoint;
    private Vector3 _hookOffset;
    private quaternion _hookRotation;

    private Action nextActionHook;

    private Vector3 _finalPos, _startPos;
    private float lerp;

    public Model(Controller controller, Rigidbody rb, Transform player, float speedRotation,
        float speedAim, float jumpForce, CinemachineTransposer normal, CinemachineTransposer zoom, Transform hand,
        Transform hook, float hookDistance, LineRenderer line)
    {
        _controller = controller;
        _rb = rb;
        _player = player;
        _speedRotation = speedRotation;
        _speedAim = speedAim;
        _normalCameraOffset = normal;
        _zoomCameraOffset = zoom;
        _jumpForce = jumpForce;
        _hand = hand;
        _hook = hook;
        _maxDistanceHook = hookDistance;
        _line = line;
        _hookOffset = _hook.transform.localPosition;
        _hookRotation = _hook.transform.localRotation;
    }

    public void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _rb.MovePosition(_player.transform.position + Vector3.Normalize(_player.transform.right * dir.x + _player.transform.forward * dir.z) * _currentSpeed * Time.fixedDeltaTime);
        _controller._view.UpdateMove(dir);
    }

    public void SetSpeed(float speed)
    {
        _currentSpeed = speed;
    }
    
    //Rotacion del personaje en el eje Y
    public void Rotate()
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X") * _speedRotation * Time.deltaTime));
    }

    //Movimiento vertical de la camara
    public void CameraAim()
    {
        Vector3  rotation = Vector3.up * -Input.GetAxis("Mouse Y") * Time.deltaTime * _speedAim;

        _normalCameraOffset.m_FollowOffset += rotation;
        _zoomCameraOffset.m_FollowOffset += rotation;

        _normalCameraOffset.m_FollowOffset.y =
            _zoomCameraOffset.m_FollowOffset.y = Mathf.Clamp(_normalCameraOffset.m_FollowOffset.y, 0, 6f);
    }

    public void Jump()
    {
        _rb.AddForce(_player.transform.up * _jumpForce);
    }
    
    public bool IsGrounded()
    {
        return Physics.Raycast(_player.position + _player.up * 0.5f, _player.up * -1, 0.6f);
    }

    //Calcula hacia donde va el gancho y deactiva funciones del Player
    public void ShootHook()
    {
        if (_isGrapping) return;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, _maxDistanceHook))
        {
            //Si se agarra a algo
            _hookPoint = hit.point;

            if(hit.collider.gameObject.layer == 12)
            {
                lerp = 0;
                _going = true;
                _startPos = _player.position;
                _startPos.y += 2;
                _finalPos = _hookPoint - _player.position;
                _finalPos *= 2;
                _finalPos += _player.position;
                _finalPos.y = _startPos.y;
                
                Debug.DrawLine(_startPos, _finalPos, Color.blue, 5);
                Debug.DrawLine(_startPos, _hookPoint, Color.green,5);
                Debug.DrawLine(_finalPos, _hookPoint, Color.green,5);
                nextActionHook = SwingHook;
            }
            else
            {
                nextActionHook = Grapping;
            }
        }
        else
        {
            //No se agarra a nada
            _hookPoint = Camera.main.transform.position + Camera.main.transform.forward * _maxDistanceHook;
            nextActionHook = FailHook;
        }

        _isGrapping = true;            
        _hook.parent = null;
        _hook.LookAt(_hookPoint);
        _rb.useGravity = false;
        _line.enabled = true;
        _controller.onFixedUpdate = MoveHook;

    }

    //Mueve el Gancho y cuando llega mueve al Player
    public void MoveHook()
    {
        _hook.position = Vector3.Lerp(_hook.position, _hookPoint, 5 * Time.fixedDeltaTime);
        if (Vector3.Distance(_hook.position, _hookPoint) < 0.5f)
        {
            _controller.onFixedUpdate = nextActionHook;
        }
        
        _line.SetPosition(0, _hand.position);
        _line.SetPosition(1, _hook.position);
    }
    
    public void Grapping()
    {
        _player.position = Vector3.Lerp(_player.position, _hookPoint - _hand.localPosition, 5 * Time.fixedDeltaTime);
            if (Vector3.Distance(_player.position, _hookPoint - _hand.localPosition) < 0.5f)
            {
                _hook.parent = _hand;
                _hook.transform.localPosition = _hookOffset;
                _hook.transform.localRotation = _hookRotation;
                _isGrapping = false;
                _line.enabled = false;
            } 
        _line.SetPosition(0, _hand.position);
        _line.SetPosition(1, _hook.position);
    }

    public void SwingHook()
    {
        Vector3 position = Vector3.Lerp(_startPos, _finalPos, lerp);
        position.y -= Mathf.Sin(lerp * Mathf.PI) * 2;

        _player.position = position;
        lerp += Time.deltaTime * 0.35f * (_going ? 1 : -1);

        if (lerp > 1 || lerp < 0) _going = !_going;

        _line.SetPosition(0, _hand.position);
        _line.SetPosition(1, _hook.position);
    }

    public void FailHook()
    {
        _hook.position = Vector3.Lerp(_hook.position, _hand.position, 5 * Time.fixedDeltaTime);
        if (Vector3.Distance(_hook.position, _hookPoint) < 0.5f)
        {
            
                _hook.parent = _hand;
                _hook.transform.localPosition = _hookOffset;
                _hook.transform.localRotation = _hookRotation;
                _controller.onFixedUpdate -= FailHook;
                _isGrapping = false;
        }
        _line.SetPosition(0, _hand.position);
        _line.SetPosition(1, _hook.position);
    }
    
    //Activa los movimietos del Player despues del Hook
    public void StopHooking()
    {
        _controller.onFixedUpdate = Move;
        _hook.parent = _hand;
        _hook.transform.localPosition = _hookOffset;
        _hook.transform.localRotation = _hookRotation;
        _isGrapping = false;
        _rb.useGravity = true;
        _line.enabled = false;
    }
}
