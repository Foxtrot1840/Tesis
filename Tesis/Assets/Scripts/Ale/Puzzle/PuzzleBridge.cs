using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum Gears
{
   Train,Graveyard,Greenhouse
}

public class PuzzleBridge : Interactuables
{
   [SerializeField] private GameObject GearTrain;
   [SerializeField] private GameObject GearGraveyard;
   [SerializeField] private GameObject GearGreenHouse;
   [SerializeField] private GameObject Bridge;

   protected override void Start()
   {
      base.Start();
      GearTrain.SetActive(false);
      GearGraveyard.SetActive(false);
      GearGreenHouse.SetActive(false);
      Bridge.SetActive(false);
   }

   protected override void Action()
   {
      if (GearGraveyard.activeSelf && GearTrain.activeSelf && GearGreenHouse.activeSelf)
      {
         Bridge.SetActive(true);
      }
      
      if (plyController.gearInventary.Contains(Gears.Train))
      {
         GearTrain.SetActive(true);
      }
      
      if(plyController.gearInventary.Contains(Gears.Graveyard))
      {
         GearGraveyard.SetActive(true);
      }
      
      if(plyController.gearInventary.Contains(Gears.Greenhouse))
      {
         GearGreenHouse.SetActive(true);
      }
   }
}
