using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
   [SerializeField] private StoryMoment[] story = new StoryMoment[2];
   [SerializeField] private TextMeshProUGUI message;
   private int actualMoment;
   public static StoryManager instance;

   private void Awake()
   {
      if (instance == null) instance = this;
      else Destroy(this);
   }

   private void Start()
   {
      actualMoment = -1;
      ChangeStory();
   }

   public void ChangeStory()
   {
      if (actualMoment >= 0)
      {
         foreach (var icon in story[actualMoment].icons)
         {
            icon.SetActive(false);
         }
      }
      actualMoment++;
      message.text = story[actualMoment].message;
      foreach (var icon in story[actualMoment].icons)
      {
         icon.SetActive(true);
      }
      Invoke("TurnOffMessage",3);
   }

   private void TurnOffMessage()
   {
      message.text = "";
   }
   
}
