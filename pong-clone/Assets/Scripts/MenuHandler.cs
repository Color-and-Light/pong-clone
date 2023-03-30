using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
   public void Load()
   {
      SceneManager.LoadScene((int)Level.MainGame);
   }
   public void Restart()
   {
      GameManager.Instance.NewGame();
   }
   public void Resume()
   {
      GameManager.Instance.IsPaused = false;
      GameManager.Instance.OnGamePause();
      Cursor.visible = false;
   }
   public void Quit()
   {
      System.Diagnostics.Process.GetCurrentProcess().Kill(); //unity method for closing applications (Application.Quit), does not work for some reason.
   }
   public void ToggleFullscreen(bool toggle) => Screen.fullScreen = toggle;
}
