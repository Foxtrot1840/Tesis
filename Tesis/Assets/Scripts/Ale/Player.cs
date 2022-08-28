using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;
    
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _speedRotation = 50;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector2 limiteCamera;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Movimiento con los inputs en x y z
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        
        _rb.MovePosition(transform.position + Vector3.Normalize(transform.right * dir.x + transform.forward * dir.z) * _speed * Time.fixedDeltaTime);
        
        //Rotacion en z dependiendo la posicion del mouse
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X") * _speedRotation));
        
        _camera.transform.Rotate(Vector3.right * -Input.GetAxis("Mouse Y") *_speedRotation );
        
        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(transform.up * _jumpForce);
        }
    }
}

