using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
   public static GameManager _Instance = null;

   [Range(0, 50)]
   public int MoveSpeed = 10;
   private void Awake()
   {
      if (_Instance == null)
      {
         _Instance = this;
      }
      else Destroy(this);
      
      DontDestroyOnLoad(this);
   }
}
