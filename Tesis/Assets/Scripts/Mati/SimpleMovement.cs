using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private float _sprintSpeed = 3;
    private float _walkSpeed = 1.5f;
    private float _smoothTime = .15f;
    private Rigidbody _rb;
    private float _speed;
   

    private float verticalLookRotation;


    private Vector3 smoothMoveVelocity;
    private Vector3 moveAmount;
    
    
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

		
		
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _walkSpeed), ref smoothMoveVelocity, _smoothTime);

    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
