using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;

public class SpiderLegsManager : MonoBehaviour
{
    public TargetIKSpider[] group1 = new TargetIKSpider[3];
    public TargetIKSpider[] group2 = new TargetIKSpider[3];
    public float speed;
    public float stepDistance;

    private void Awake()
    {
        group1.Select(x => x.manager = this).ToArray();
        group1.Select(x => x.Movement = true).ToArray();
        group2.Select(x => x.manager = this).ToArray();
    }

    public void CheckLegs(TargetIKSpider leg)
    {
        if (group1.Contains(leg))
        {
            if (group1.All(x=>x.Movement == false))
            {
                group2.Select(x => x.Movement = true).ToArray();
            }
        }
        else
        {
            if (group2.All(x => x.Movement == false))
            {
                group1.Select(x => x.Movement = true).ToArray();
            }
        }
    }
    
}
