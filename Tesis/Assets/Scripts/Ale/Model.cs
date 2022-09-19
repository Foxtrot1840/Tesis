using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using TMPro;

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
    
    private bool _isGrapping, _isHooked;
    private Vector3 _hookPoint;

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
            _zoomCameraOffset.m_FollowOffset.y = Mathf.Clamp(_normalCameraOffset.m_FollowOffset.y, 0.5f, 3);
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

        if (Physics.Raycast(_hand.position, Camera.main.transform.forward, out RaycastHit hit, _maxDistanceHook))
        {
            _hookPoint = hit.point;
            _isGrapping = true;
            _isHooked = false;
            _hook.parent = null;
            _hook.LookAt(_hookPoint);
            _controller.onFixedUpdate -= Move;
            _controller.onFixedUpdate += Grapping;
            _rb.useGravity = false;
            _line.enabled = true;
        }
    }

    //Mueve el Gancho y cuando llega mueve al Player
    public void Grapping()
    {
        Debug.Log("Grapping");
        _hook.position = Vector3.Lerp(_hook.position, _hookPoint, 5 * Time.fixedDeltaTime);
        if (Vector3.Distance(_hook.position, _hookPoint) < 0.5f)
        {
            _player.position = Vector3.Lerp(_player.position, _hookPoint - _hand.localPosition, 5 * Time.fixedDeltaTime);
            if (Vector3.Distance(_player.position, _hookPoint - _hand.localPosition) < 0.5f)
            {
                _hook.parent = _hand;
                _hook.localPosition = Vector3.zero;
                _isGrapping = false;
                _isHooked = true;
                _controller.onFixedUpdate -= Grapping;
            }
        }
        _line.SetPosition(0, _hand.position);
        _line.SetPosition(1, _hookPoint);
    }
    
    //Activa los movimietos del Player despues del Hook
    public void StopHooking()
    {
        _controller.onFixedUpdate -= Grapping;
        _controller.onFixedUpdate += Move;
        _hook.parent = _hand;
        _hook.localPosition = Vector3.zero;
        _isGrapping = false;
        _rb.useGravity = true;
        _line.enabled = false;
    }
    
}
