using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance = null;
   public delegate void SystemCallbacks();
   private SystemCallbacks systemCallbacks;
   public GameObject puck;
   private IPuck puckBase;
   private bool hasStarted;
   private float roundTimer = 2f;
   public Vector2 puckSpeed;

   [Range(0, 50)]
   public int MoveSpeed = 10;
   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else Destroy(this);
      
      DontDestroyOnLoad(this);
      
      #region delegate callbacks

     /*
     systemCallbacks += OnScoreLeft();
     systemCallbacks += OnScoreRight();
     systemCallbacks += OnBounceWall();
     systemCallbacks += OnBouncePaddle();

     SystemCallbacks OnScoreLeft()
     {
        throw new NotImplementedException();
     }
      SystemCallbacks OnScoreRight()
     {
        throw new NotImplementedException();
     }

     SystemCallbacks OnBounceWall()
     {
        throw new NotImplementedException();
     }

     SystemCallbacks OnBouncePaddle()
     {
        throw new NotImplementedException();
     }*/
      
      #endregion
      
   }

   private void Update()
   {
      if(SceneManager.GetActiveScene().buildIndex == (int)Level.MainGame)
      {
         roundTimer -= Time.time;
         if (roundTimer > 0 && !hasStarted)
         {
            roundTimer -= Time.time;
            Debug.Log("The time is " + roundTimer);
         }

         else if (!hasStarted)
         {
            Instantiate(puck, Vector2.zero, Quaternion.identity);
            hasStarted = true;
            puckBase = puck.GetComponent<IPuck>();
            puckBase.Punch();

         }
      }
   }

   
}
