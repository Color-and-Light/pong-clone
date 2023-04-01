using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
   //global fields
   public static GameManager Instance = null;
   public delegate void ScoreCallback(GameObject sender);
   public ScoreCallback ScoreCallbacks;
   public delegate void BounceCallback();
   public BounceCallback BounceCallbacks;
   public Vector2 puckDirection;
   public readonly int MaxHorizontalSpeed = 55;
   public enum LastScored { None, Left, Right }
   [Range(0, 50)] public int MoveSpeed = 15;

   //props
   [field:SerializeField] public float PuckSpeed { get; private set; } 
   public int LeftScore { get; private set; } 
   public int RightScore { get; private set; }
   public bool IsPaused { get; set; }
   public LastScored LastScore { get; private set; } = LastScored.None;

   //local fields
   [SerializeField] private GameObject _uiCanvasObject, _winCanvasObject, _pauseCanvasObject, _puck;
   private GameObject _uiCanvas, _winCanvas, _pauseCanvas, _leftScoreText, _rightScoreText;
   private IPuck _puckBase;
   private bool _hasStarted, _resetScore;
   private bool _canPause = true;

   private void Awake()
   {
      if (Instance != null)
      {
         Destroy(this);
      }
      Instance = this;
      DontDestroyOnLoad(this);

      _uiCanvas = Instantiate(_uiCanvasObject, Vector2.zero, Quaternion.identity); //initializing UI at runtime
      _winCanvas = Instantiate(_winCanvasObject, Vector2.zero, Quaternion.identity);
      _pauseCanvas = Instantiate(_pauseCanvasObject, Vector2.zero, Quaternion.identity);
      _uiCanvas.SetActive(false);
      _winCanvas.SetActive(false);
      _pauseCanvas.SetActive(false);
      _rightScoreText = _uiCanvas.transform.GetChild(0).gameObject;
      _leftScoreText = _uiCanvas.transform.GetChild(1).gameObject;
      
      DontDestroyOnLoad(_uiCanvas);
      DontDestroyOnLoad(_winCanvas);
      DontDestroyOnLoad(_pauseCanvas);

      ScoreCallbacks += OnScore;
      ScoreCallbacks += OnScoreAudio;
      ScoreCallbacks += OnRoundReset;
      BounceCallbacks += OnBounce;
   }
   
   #region Delegate Callbacks
   void OnScore(GameObject sender)
   {
      if(sender.CompareTag("LeftScoreBox")) //not a fan of string comparisons, but this is the only time it's used.
      {
         LeftScore++;
         sender.GetComponent<ScoreBoxText>().scoreField.GetComponent<TMP_Text>().text = LeftScore.ToString();
         LastScore = LastScored.Left;
      }
      else
      {
         RightScore++;
         sender.GetComponent<ScoreBoxText>().scoreField.GetComponent<TMP_Text>().text = RightScore.ToString();
         LastScore = LastScored.Right;
      }
   }
     
   void OnScoreAudio(GameObject sender) => AudioManager.Instance.PlayScoreSound(); 
   
   void OnRoundReset(GameObject sender)
   {
      if(LeftScore < 11 && RightScore < 11) { StartNewRound();}
      else
      {
         WinGame();
      }
   }

   void OnBounce() => AudioManager.Instance.PlayBounceSound();

   #endregion

   void StartNewRound() => StartCoroutine(KickoffTimer(1f));

   IEnumerator KickoffTimer(float timeToWait)
   {
      _winCanvas.SetActive(false); //redundancy checks
      _uiCanvas.SetActive(true);
      GameObject _puckHandler = Instantiate(_puck, Vector2.zero, Quaternion.identity);
      _hasStarted = true;
      _puckBase = _puckHandler.GetComponent<IPuck>();
      _puckBase.Init();
      yield return new WaitForSeconds(timeToWait);
      _puckBase.Punch();
   }
   void WinGame()
   {
      _canPause = false;
      Cursor.visible = true;
      _winCanvas.SetActive(true);
      _uiCanvas.SetActive(false);
      var text = _winCanvas.GetComponentInChildren<TMP_Text>();
      if (RightScore >= 11)
      {
         text.text = "Player 1 Wins!";
      }
      else
      {
         text.text = "Player 2 Wins!";
      }
      AudioManager.Instance.Win();
   }

   public void NewGame()
   {
      LeftScore = 0;
      RightScore = 0;
      _leftScoreText.GetComponent<TMP_Text>().text = LeftScore.ToString();
      _rightScoreText.GetComponent<TMP_Text>().text = RightScore.ToString();
      _canPause = true;
      Cursor.visible = false;
      StartNewRound();
   }

   public void OnGamePause()
   {
      if (SceneManager.GetActiveScene().buildIndex == (int)Level.MainGame && !IsPaused && _canPause)
      {
         IsPaused = true;
         _pauseCanvas.SetActive(!_pauseCanvas.activeSelf);
         _uiCanvas.SetActive(!_uiCanvas.activeSelf);
         Time.timeScale = _pauseCanvas.activeSelf ? 0 : 1;
         float pauseTime = 1f;
         Cursor.visible = !Cursor.visible;
         StartCoroutine(PauseTime(pauseTime));
      }
   }

   IEnumerator PauseTime(float pauseTime)
   {
      yield return new WaitForSeconds(pauseTime);
      IsPaused = false;
   }

   private void Update()
   {
      if(SceneManager.GetActiveScene().buildIndex == (int)Level.MainGame)
      {
         if (!_hasStarted)
         {
            StartNewRound();
         }
      }
   }
}
