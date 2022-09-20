using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float movement;
    [SerializeField] private float movementSpeed;
    private static readonly int Move = Animator.StringToHash("Blend");
    public GameObject sword;
    public GameObject gun;

    [SerializeField] private bool isAiming;

    void Start()
    {
        _animator = GetComponent<Animator>();
        isAiming = false;
        gun.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            movement += Time.deltaTime * movementSpeed;
            _animator.SetFloat(Move, movement);

            if (movement >= 1.9f) movement = 2;
        }

        if (!Input.anyKey)
        {
            movement -= Time.deltaTime * movementSpeed;
            _animator.SetFloat(Move, movement);
            if (movement < 0 ) movement = 0;
        }
        
        Aim();
        
    }

    void Aim()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
            isAiming = !isAiming;
        sword.gameObject.SetActive(!isAiming);
        gun.gameObject.SetActive(isAiming);
        _animator.SetLayerWeight(1, isAiming ? 1 : 0);
    }
}
