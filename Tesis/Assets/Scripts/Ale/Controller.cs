using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Controller : Entity
{
    [SerializeField] private CinemachineVirtualCamera _normalCamera;
    [SerializeField] private CinemachineVirtualCamera _zoomCamera;
    [SerializeField] private float _speedAimRotation;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sprint = 10;
    [SerializeField] private float _speedRotation = 50;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _shootPoint;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Transform _hook;
    [SerializeField] private Transform _hand;
    [SerializeField] private float _hookDistance;
    [SerializeField] private LineRenderer _line;

    private CinemachineTransposer _normalCameraAim;
    private CinemachineTransposer _zoomCameraAim;
    private Rigidbody _rb;
    private Animator _anim;
    private Model _model;
    public View _view;

    private bool _isZoom = false;

    public event Action onFixedUpdate = delegate{ };

    private void Awake()
    {
        _normalCameraAim = _normalCamera.GetCinemachineComponent<CinemachineTransposer>();
        _zoomCameraAim = _zoomCamera.GetCinemachineComponent<CinemachineTransposer>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _model = new Model(this, _rb, this.transform, _speed, _sprint, _speedRotation, _speedAimRotation, _jumpForce, _normalCameraAim,
                           _zoomCameraAim, _hand, _hook, _hookDistance, _line);
        _view = new View(_anim);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        onFixedUpdate += _model.Move;
    }

    private void Update()
    {
        _model.Rotate();
        _model.CameraAim();

        //Con click derecho se invierte el Zoom
        if (Input.GetMouseButtonDown(1))
        {
            _isZoom = !_isZoom;
            _canvas.SetActive(_isZoom);
            _anim.SetBool("Zoom",_isZoom);
        }

        //El click izquierdo se realiza la animacion de ataque
        ////(el animator pregunta si se apunta se hace un disparo, sino se usa la espada) 
        if (Input.GetMouseButtonDown(0))
        {
            _view.Attack();
        }
        
        if (Input.GetButtonDown("Jump") && _model.IsGrounded())
        {
            _view.Jump();
            _model.Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _model.ShootHook();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            _model.StopHooking();
        }
    }

    private void FixedUpdate()
    {
        onFixedUpdate();
    }

    public void Shoot()
    {
        Instantiate(_bullet, _shootPoint.transform.position, Quaternion.Euler(_zoomCamera.transform.rotation.eulerAngles + new Vector3(-1,3.5f,0)));
    }
    
    public void ResetJump()
    {
        _anim.ResetTrigger("Jump");
    }
}
