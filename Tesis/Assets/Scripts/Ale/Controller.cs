    using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Controller : Entity
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private CinemachineVirtualCamera _normalCamera;
    [SerializeField] private CinemachineVirtualCamera _zoomCamera;
    [SerializeField] private float _speedAimRotation;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sprint = 10;
    [SerializeField] private float _speedRotation = 50;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private GameObject _shootParticles;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private LayerMask _layerShoot;
    [SerializeField] private Transform _hook;
    [SerializeField] private Transform _hand;
    [SerializeField] private float _hookDistance;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private float viewAngle;

    private CinemachineTransposer _normalCameraAim;
    private CinemachineTransposer _zoomCameraAim;
    private Rigidbody _rb;
    private Animator _anim;
    private Model _model;
    public View _view;

    private bool _isZoom = false;

    public LayerMask hookLayers;
    public List<Enum> gearInventary = new List<Enum>();
    public Vector3 lastSavePoint;

    public Action onFixedUpdate = delegate{ };
    public event Action interactables;
    public List<Vector3> hookSwingPoint;

    private void Awake()
    {
        _normalCameraAim = _normalCamera.GetCinemachineComponent<CinemachineTransposer>();
        _zoomCameraAim = _zoomCamera.GetCinemachineComponent<CinemachineTransposer>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _model = new Model(this, _rb, this.transform, _speedRotation, _speedAimRotation, _jumpForce, _normalCameraAim,
                           _zoomCameraAim, _hand, _hook, _hookDistance, _line);
        _view = new View(_anim);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        currentHealth = _maxHealth;
        onFixedUpdate += _model.Move;
        _model.SetSpeed(_speed);
    }

    private void Update()
    {
        _model.Rotate();
        _model.CameraAim();

        //Con click derecho se invierte el Zoom
        if (Input.GetMouseButtonDown(1))
        {
            _isZoom = !_isZoom;
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
            Sprint(false);
            _view.Jump();
            _model.Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactables != null)
            {
                interactables();
            }
            else if(FieldOfView() != Vector3.zero)
            {
                _model.hookPoint = FieldOfView();
                _model.nextActionHook = _model.SwingHook;
                _model.StartHooking();
            }
            else
            {
                _model.ShootHook();
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            _model.StopHooking();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))Sprint(true);

        if (Input.GetKeyUp(KeyCode.LeftShift)) Sprint(false); 
    }

    private void FixedUpdate()
    {
        onFixedUpdate();
    }

    public void Shoot()
    {
        Destroy(Instantiate(_shootParticles, _shootPoint.position, _shootPoint.rotation), 2);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 30,
                _layerShoot))
        {
            IDamagable d = hit.collider.GetComponent<IDamagable>();
            if (d != null)
            {
                d.GetDamage(1, hit.point, hit.normal);
            }
        }
    }

    public void Sprint(bool active)
    {
        if (active)
        {
            _model.SetSpeed(_sprint);
        }
        else
        {
            _model.SetSpeed(_speed);
        }
        _view.Sprint(active);
    }
    
    public void ResetJump()
    {
        _anim.ResetTrigger("Jump");
    }

    private Vector3 FieldOfView()
    {
        foreach (var point in hookSwingPoint)
        {
            Vector3 dir = point - transform.position;
            if (Vector3.Angle(Camera.main.transform.forward, dir) <= viewAngle / 2)
            {
                return point;
            }
        }
        return Vector3.zero;
    }
    
    public override void GetDamage(int damage)
    {
        currentHealth -= damage;
        _view.Hit();
        EventManager.TriggerEvent(EventManager.EventsType.Event_GetDamage, currentHealth, _maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_FinishGame, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _hookDistance);
    }
}
