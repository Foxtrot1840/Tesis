using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMoment : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            StoryManager.instance.ChangeStory();
            Destroy(this);
        }
    }
}
