using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View
{
    private Animator _anim;
    
    
    public View(Animator animator)
    {
        _anim = animator;
    }

    public void UpdateMove()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _anim.SetFloat("MovX", dir.x);
        _anim.SetFloat("MovY", dir.z);
    }

    public void Jump()
    {
        _anim.SetTrigger("Jump");
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }
}
