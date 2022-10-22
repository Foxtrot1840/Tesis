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

    public void UpdateMove(Vector3 dir)
    {
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

    public void Sprint(bool sprint)
    {
        _anim.SetBool("Sprint",sprint);
    }

    public void Hit()
    {
        _anim.SetTrigger("GetDamage");
    }

    public void ActiveAnimator(bool active)
    {
        _anim.enabled = active;
    }
}