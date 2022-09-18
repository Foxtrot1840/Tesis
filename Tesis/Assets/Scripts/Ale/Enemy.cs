using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
   [SerializeField] private int _maxHealth;

   private void Start()
   {
      currentHealth = _maxHealth;
   }
}
