using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
   public static GameManager instance = null;
   public delegate void ScoreCallback(Rigidbody2D rb);
   public ScoreCallback scoreCallbacks;
   public GameObject puck;
   private IPuck puckBase;
   private bool hasStarted;
   private float roundTimer = 1000f;
   public Vector2 puckSpeed;
   private TMP_Text leftScoreText;
   private TMP_Text rightScoreText;

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

      scoreCallbacks += OnScore;
     // systemCallbacks += OnBounceWall();
    // systemCallbacks += OnBouncePaddle();

     void OnScore(Rigidbody2D rb)
     {
        AudioManager.instance.Score();
     }

     //SystemCallbacks OnBounceWall()
    //{
    //   throw new NotImplementedException();
    //}

    //SystemCallbacks OnBouncePaddle()
    //{
    //   throw new NotImplementedException();
    //}
      
      #endregion
      
   }

   private void Update()
   {
      if(SceneManager.GetActiveScene().buildIndex == (int)Level.MainGame)
      {
         if (roundTimer > 0 && !hasStarted)
         {
            roundTimer -= Time.time;
         }

         else if (!hasStarted)
         {
            GameObject puckHandler = Instantiate(puck, Vector2.zero, Quaternion.identity);
            hasStarted = true;
            puckBase = puckHandler.GetComponent<IPuck>();
            puckBase.Init();
            puckBase.Punch();

         }
      }
   }

   
}
