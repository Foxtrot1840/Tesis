using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearCollectable : Interactuables
{
    [SerializeField] private Gears type;
    
    protected override void Action()
    {
       plyController.gearInventary.Add(type);

       if (plyController.gearInventary.Count >= 3)
       {
           StoryManager.instance.ChangeStory();
       }
       Destroy(gameObject);
    }
}
