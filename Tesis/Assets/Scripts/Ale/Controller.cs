using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _speedAimRotation;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _speedRotation = 50;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _shootPoint;
    
    private Rigidbody _rb;
    private Animator _anim;
    private Model _model;
    private View _view;

    private bool _isZoom;

    private void Awake()
    {
        _model = new Model(_rb, this.gameObject, _speed);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _model.Move(dir);
    }
}
