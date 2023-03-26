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
   private bool hasStarted, resetScore;
   private float roundTimer = 1500f;
   public bool isPaused;
   private bool canPause = true;
   public Vector2 puckDirection;
   public float puckSpeedScalar;
   private GameObject leftScoreText, rightScoreText;
   [SerializeField] private GameObject uiCanvasObject, winCanvasObject, pauseCanvasObject;
   private GameObject uiCanvas, winCanvas, pauseCanvas;

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

      uiCanvas = Instantiate(uiCanvasObject, Vector2.zero, Quaternion.identity); //initializing UI at runtime
      winCanvas = Instantiate(winCanvasObject, Vector2.zero, Quaternion.identity);
      pauseCanvas = Instantiate(pauseCanvasObject, Vector2.zero, Quaternion.identity);
      uiCanvas.SetActive(false);
      winCanvas.SetActive(false);
      pauseCanvas.SetActive(false);
      rightScoreText = uiCanvas.transform.GetChild(0).gameObject;
      leftScoreText = uiCanvas.transform.GetChild(1).gameObject;
      
      DontDestroyOnLoad(uiCanvas);
      DontDestroyOnLoad(winCanvas);
      DontDestroyOnLoad(pauseCanvas);

      scoreCallbacks += OnScore;
      scoreCallbacks += OnScoreAudio;
      scoreCallbacks += OnRoundReset;

      bounceCallbacks += OnBounce;
   }
   
   #region Delegate Callbacks
   void OnScore(GameObject sender)
   {
      if(sender.CompareTag("LeftScoreBox")) //not a fan of string comparisons, but this is the only time it's used.
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
     
   void OnScoreAudio(GameObject sender) => AudioManager.instance.PlayScoreSound(); 
   
   void OnRoundReset(GameObject sender)
   {
      if(leftScore < 11 && rightScore < 11) { StartNewRound();}
      else
      {
         WinGame();
      }
   }
   void OnBounce()
   {
      AudioManager.instance.PlayBounceSound();
   }

   #endregion
   void StartNewRound()
   {
      winCanvas.SetActive(false); //redundancy checks
      uiCanvas.SetActive(true);
      GameObject puckHandler = Instantiate(puck, Vector2.zero, Quaternion.identity);
      hasStarted = true;
      puckBase = puckHandler.GetComponent<IPuck>();
      puckBase.Init();
      puckBase.Punch();
   }

   void WinGame()
   {
      canPause = false;
      Cursor.visible = true;
      winCanvas.SetActive(true);
      uiCanvas.SetActive(false);
      var text = winCanvas.GetComponentInChildren<TMP_Text>();
      if (rightScore >= 11)
      {
         text.text = "Player 1 Wins!";
      }
      else
      {
         text.text = "Player 2 Wins!";
      }
      AudioManager.instance.Win();
   }

   public void NewGame()
   {
      leftScore = 0;
      rightScore = 0;
      leftScoreText.GetComponent<TMP_Text>().text = leftScore.ToString();
      rightScoreText.GetComponent<TMP_Text>().text = rightScore.ToString();
      canPause = true;
      StartNewRound();
   }

   public void OnGamePause()
   {
      if (SceneManager.GetActiveScene().buildIndex == (int)Level.MainGame && !isPaused && canPause)
      {
         isPaused = true;
         pauseCanvas.SetActive(!pauseCanvas.activeSelf);
         Time.timeScale = pauseCanvas.activeSelf ? 0 : 1;
         float pauseTime = 1f;
         Cursor.visible = !Cursor.visible;
         StartCoroutine(PauseTime(pauseTime));

      }
   }

   IEnumerator PauseTime(float pauseTime)
   {
      yield return new WaitForSeconds(pauseTime);
      isPaused = false;
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
