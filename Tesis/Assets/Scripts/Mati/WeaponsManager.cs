using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Zoom = Animator.StringToHash("Zoom");

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKeyDown(KeyCode.Mouse0)) return;

        if (!_animator.GetBool(Zoom))
        {
            _animator.SetTrigger(Attack);
        }
        
    }
}
