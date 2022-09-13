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
    private float _speed;
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

    private bool _isGrapping;
    private Vector3 _hookPoint;

    public Model(Controller controller, Rigidbody rb, Transform player, float speed, float speedRotation,
        float speedAim, float jumpForce, CinemachineTransposer normal, CinemachineTransposer zoom, Transform hand,
        Transform hook, float hookDistance, LineRenderer line)
    {
        _controller = controller;
        _rb = rb;
        _player = player;
        _speed = speed;
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
        Debug.Log("Move");
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _rb.MovePosition(_player.transform.position + Vector3.Normalize(_player.transform.right * dir.x + _player.transform.forward * dir.z) * _speed * Time.fixedDeltaTime);
    }

    public void Rotate()
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X") * _speedRotation * Time.deltaTime));
    }

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

    public void ShootHook()
    {
        if (_isGrapping) return;

        if (Physics.Raycast(_hand.position, Camera.main.transform.forward, out RaycastHit hit, _maxDistanceHook))
        {
            _hookPoint = hit.point;
            _isGrapping = true;
            _hook.parent = null;
            _hook.LookAt(_hookPoint);
            _controller.onFixedUpdate -= Move;
            _controller.onFixedUpdate += Grapping;
            _rb.useGravity = false;
            _line.enabled = true;
        }
    }

    public void Grapping()
    {
        Debug.Log("Grapping");
        _hook.position = Vector3.Lerp(_hook.position, _hookPoint, 5 * Time.fixedDeltaTime);
        if (Vector3.Distance(_hook.position, _hookPoint) < 0.5f)
        {
            _player.position = Vector3.Lerp(_player.position, _hookPoint - _hand.localPosition, 5 * Time.fixedDeltaTime);
            if (Vector3.Distance(_player.position, _hookPoint - _hand.localPosition) < 0.5f)
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
        _line.SetPosition(0, _hand.position);
        _line.SetPosition(1, _hookPoint);
    }
    
}
