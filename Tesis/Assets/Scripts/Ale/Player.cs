using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;
    
    private CinemachineTransposer _normalCameraOffset;
    private CinemachineTransposer _zoomCameraOffset;

    [SerializeField] private CinemachineVirtualCamera _normalCamera;
    [SerializeField] private CinemachineVirtualCamera _zoomCamera;
    [SerializeField] private float _speedAimRotation;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _speedRotation = 50;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _shootPoint;
    private bool _isZoom;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _normalCameraOffset = _normalCamera.GetCinemachineComponent<CinemachineTransposer>();
        _zoomCameraOffset = _zoomCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _anim.SetTrigger("Jump");
            _rb.AddForce(transform.up * _jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _anim.SetTrigger("Attack");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            _isZoom = !_isZoom;
            _anim.SetBool("Zoom",_isZoom);
        }
    }

    void FixedUpdate()
    {
        //Movimiento con los inputs en x y z
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        
        _anim.SetFloat("MovX", dir.x);
        _anim.SetFloat("MovY", dir.z);
        
        _rb.MovePosition(transform.position + Vector3.Normalize(transform.right * dir.x + transform.forward * dir.z) * _speed * Time.fixedDeltaTime);
        
        //Rotacion en z dependiendo la posicion del mouse
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X") * _speedRotation));

        _normalCameraOffset.m_FollowOffset += Vector3.up *  -Input.GetAxis("Mouse Y") * Time.deltaTime * _speedAimRotation;
        _zoomCameraOffset.m_FollowOffset += Vector3.up *  -Input.GetAxis("Mouse Y") * Time.deltaTime * _speedAimRotation;

        _normalCameraOffset.m_FollowOffset.y = Mathf.Clamp(_normalCameraOffset.m_FollowOffset.y, 0.5f, 3);
        _zoomCameraOffset.m_FollowOffset.y = Mathf.Clamp(_normalCameraOffset.m_FollowOffset.y, 0.5f, 3);
    }

    public void Shoot()
    {
        Instantiate(_bullet, _shootPoint.transform.position, Quaternion.Euler(new Vector3(0,_shootPoint.transform.rotation.eulerAngles.y +150 , 90)));
    }
}
;