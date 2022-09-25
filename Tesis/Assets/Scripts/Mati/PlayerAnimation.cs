using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Standalone Variables

      private Animator _animator;
      private float _velocityX;
      private float _velocityZ;
      private static readonly int VelocityX = Animator.StringToHash("Velocity X");
      private static readonly int VelocityZ = Animator.StringToHash("Velocity Z");

    #endregion
    
    
    [SerializeField] private float _accelerationX = .4f;
    [SerializeField] private float _decelerationX = .5f;
    [SerializeField] private float _accelerationZ = .4f;
    [SerializeField] private float _decelerationZ = .5f;
    private readonly float _maximumWalkVelocityZ = .5f;
    private readonly float _maximumRunVelocityZ = 2f;

    private bool _forwardPressed;
    private bool _leftPressed;
    private bool _rightPressed;
    private bool _runPressed;


    void Start()
    {
        _animator = GetComponent<Animator>();
        
        //high _velocityZ = Back / Forth
        //high _velocityX = Strafe
    }

    private void Update()
    {
        _forwardPressed = Input.GetKey(KeyCode.W);
        _leftPressed = Input.GetKey(KeyCode.A);
        _rightPressed = Input.GetKey(KeyCode.D);
        _runPressed = Input.GetKey(KeyCode.LeftShift);

        MovementUpdate(_forwardPressed, _leftPressed, _rightPressed, _runPressed);
        ResetMovement(_forwardPressed, _leftPressed, _rightPressed, _runPressed);

        _animator.SetFloat(VelocityX, _velocityX);
        _animator.SetFloat(VelocityZ, _velocityZ);
    }

    private void MovementUpdate(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed)
    {
       
        
        var currentVelocity = runPressed ? _maximumRunVelocityZ : _maximumWalkVelocityZ;

        if (forwardPressed && _velocityZ < currentVelocity) _velocityZ += Time.deltaTime * _accelerationZ;
        
        if (forwardPressed && _velocityZ > currentVelocity) _velocityZ -= Time.deltaTime * _accelerationZ;

        if (leftPressed && _velocityX > -currentVelocity) _velocityX -= Time.deltaTime * _accelerationX;
        
        if (leftPressed && _velocityX < -currentVelocity) _velocityX += Time.deltaTime * _accelerationX;

        if (rightPressed && _velocityX < currentVelocity) _velocityX += Time.deltaTime * _accelerationX;
        
        if (rightPressed && _velocityX > currentVelocity) _velocityX -= Time.deltaTime * _accelerationX;

        
    }

    private void ResetMovement(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed)
    {
        if (!forwardPressed && _velocityZ > 0.0f) _velocityZ -= Time.deltaTime * _decelerationZ;

        if (!forwardPressed && _velocityZ < 0.0f) _velocityZ = 0.0f;

        if (!leftPressed && _velocityX < 0.0f) _velocityX += Time.deltaTime * _decelerationX;

        if (!rightPressed && _velocityX > 0.0f) _velocityX -= Time.deltaTime * _decelerationX;

        if (!leftPressed && !rightPressed && _velocityX != 0.0f && _velocityX > -0.05f && _velocityX < .05f)
            _velocityX = 0.0f;
    }
}
