using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
   [SerializeField] private StoryMoment[] story = new StoryMoment[2];
   [SerializeField] private Text message;
   private int actualMoment;
   private void Start()
   {
      actualMoment = -1;
      ChangeStory();
   }

   void ChangeStory()
   {
      if (actualMoment > 0)
      {
         foreach (var icon in story[actualMoment].icons)
         {
            icon.SetActive(false);
         }
      }
      actualMoment++;
      message.GetComponent<GameObject>().SetActive(true);
      message.text = story[actualMoment].message;
      foreach (var icon in story[actualMoment].icons)
      {
         icon.SetActive(true);
      }
      Invoke("TurnOffMessage",3);
   }

   private void TurnOffMessage()
   {
      message.GetComponent<GameObject>().SetActive(true);
   }
   
}
