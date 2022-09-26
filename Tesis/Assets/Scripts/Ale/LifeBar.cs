using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
   [SerializeField] private Image lifeBar;

   private void OnEnable()
   {
      EventManager.Subscribe(EventManager.EventsType.Event_GetDamage, UpdateLife);
   }

   private void OnDisable()
   {
      EventManager.Unsubscribe(EventManager.EventsType.Event_GetDamage, UpdateLife);
   }
   
   private void UpdateLife(params object[] p)
   {
      float a = (int)p[0];
      float b = (int)p[1];
      float amount = a / b;
      lifeBar.fillAmount = amount;
   }
}
