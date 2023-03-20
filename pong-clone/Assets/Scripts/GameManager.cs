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
   public delegate void ScoreCallback(GameObject sender);
   public ScoreCallback scoreCallbacks;
   public GameObject puck;
   private IPuck puckBase;
   public int leftScore { get; private set; }
   public int rightScore { get; private set; }
   private bool hasStarted;
   private float roundTimer = 1000f;
   public Vector2 puckSpeed;
   private GameObject leftScoreText;
   private GameObject rightScoreText;

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
      scoreCallbacks += OnScoreAudio;
      scoreCallbacks += OnRoundReset;
     // systemCallbacks += OnBounceWall();
    // systemCallbacks += OnBouncePaddle();
    
     void OnScore(GameObject sender)
     {
        if(sender.CompareTag("LeftScoreBox"))
        {
           leftScore++;
           sender.GetComponent<ScoreBoxText>().scoreField.GetComponent<TMP_Text>().text = leftScore.ToString();

        }
        else
        {
           rightScore++;
           sender.GetComponent<ScoreBoxText>().scoreField.GetComponent<TMP_Text>().text = rightScore.ToString();

        }
     }
     
     void OnScoreAudio(GameObject sender)
     {
        AudioManager.instance.Score();
        
     }

     void OnRoundReset(GameObject sender)
     {
        StartNewRound();
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
   void StartNewRound()
   {
      GameObject puckHandler = Instantiate(puck, Vector2.zero, Quaternion.identity);
      hasStarted = true;
      puckBase = puckHandler.GetComponent<IPuck>();
      puckBase.Init();
      puckBase.Punch();
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
            StartNewRound();
         }
      }
   }

   
}
