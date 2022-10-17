using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearCollectable : Interactuables
{
    [SerializeField] private Gears type;
    
    protected override void Action()
    {
       plyController.gearInventary.Add(type);
       Destroy(gameObject);
    }
}
