using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiderBulb : MonoBehaviour,IDamagable
{
    public void GetDamage(int damage)
    {
        SoundManager.instance.Play(SoundID.Glass);
        transform.parent.GetComponent<Spider>().Die();
    }

    public void GetDamage(int damage, Vector3 point, Vector3 normal)
    {
        SoundManager.instance.Play(SoundID.Glass);
        transform.parent.GetComponent<Spider>().Die();
    }
}
