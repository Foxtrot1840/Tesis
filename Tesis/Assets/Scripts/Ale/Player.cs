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
    public static Player instance;
    
    private Rigidbody _rb;
    private Animator _anim;
    
    private CinemachineTransposer _normalCameraOffset;
    private CinemachineTransposer _zoomCameraOffset;

     public CinemachineVirtualCamera _normalCamera;
     public CinemachineVirtualCamera _zoomCamera;
    [SerializeField] private float _speedAimRotation;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _speedRotation = 50;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _sword;
    public bool _isZoom;

    private void Awake()
    {
        if (instance != null) instance = this;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _normalCameraOffset = _normalCamera.GetCinemachineComponent<CinemachineTransposer>();
        _zoomCameraOffset = _zoomCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
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
            _aim.SetActive(_isZoom);
            _sword.SetActive(!_isZoom);
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
        Instantiate(_bullet, _shootPoint.transform.position, Quaternion.Euler(_zoomCamera.transform.rotation.eulerAngles + new Vector3(-1,3.5f,0)));
        //Instantiate(_bullet, _shootPoint.transform.position, Quaternion.Euler(new Vector3(0,_shootPoint.transform.rotation.eulerAngles.y +150 , 90)));
    }

    bool IsGrounded()
    {
        Debug.DrawLine(transform.position + transform.up * 0.5f,transform.position + transform.up*-1*0.6f, Color.cyan,5f);
        return Physics.Raycast(transform.position + transform.up * 0.5f, transform.up * -1, 0.6f);
    }
}
;