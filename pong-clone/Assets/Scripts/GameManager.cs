using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
   public static GameManager instance = null;
   public delegate void ScoreCallback(GameObject sender);
   public ScoreCallback scoreCallbacks;

   public delegate void BounceCallback();
   public BounceCallback bounceCallbacks;
   public GameObject puck;
   private IPuck puckBase;
   public int leftScore { get; private set; }
   public int rightScore { get; private set; }
   private bool hasStarted;
   private float roundTimer = 1000f;
   public Vector2 puckDirection;
   public float puckSpeedScalar;
   private GameObject leftScoreText, rightScoreText;
   [SerializeField] private GameObject uiCanvasObject, winCanvasObject;
   private GameObject uiCanvas, winCanvas;
   
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

      uiCanvas = Instantiate(uiCanvasObject, Vector2.zero, Quaternion.identity);
      winCanvas = Instantiate(winCanvasObject, Vector2.zero, Quaternion.identity);
      uiCanvas.SetActive(false);
      winCanvas.SetActive(false);
      rightScoreText = uiCanvas.transform.GetChild(0).gameObject;
      leftScoreText = uiCanvas.transform.GetChild(1).gameObject;
      
      DontDestroyOnLoad(uiCanvas);
      DontDestroyOnLoad(winCanvas);

      scoreCallbacks += OnScore;
      scoreCallbacks += OnScoreAudio;
      scoreCallbacks += OnRoundReset;

      bounceCallbacks += OnBounce;
   }
   
   #region Delegate Callbacks
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
      if(leftScore < 3 && rightScore < 3) { StartNewRound();}
      else
      {
         WinGame();
      }
   }

   void OnBounce()
   {
      AudioManager.instance.Bounce();
   }

   #endregion
   void StartNewRound()
   {
      uiCanvas.SetActive(true);
      GameObject puckHandler = Instantiate(puck, Vector2.zero, Quaternion.identity);
      hasStarted = true;
      puckBase = puckHandler.GetComponent<IPuck>();
      puckBase.Init();
      puckBase.Punch();
   }

   void WinGame()
   {
      winCanvas.SetActive(true);
      uiCanvas.SetActive(false);
      var text = winCanvas.GetComponentInChildren<TMP_Text>();
      if (leftScore >= 11)
      {
         text.text = "Player 1 Wins!";
      }
      else
      {
         text.text = "Player 2 Wins!";
      }
      AudioManager.instance.Win();
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
