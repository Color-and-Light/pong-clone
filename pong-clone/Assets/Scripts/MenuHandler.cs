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
   public void MainMenu()
   {
      CleanupSingletons();
      SceneManager.LoadScene((int)Level.MainMenu);
   }

   public void Restart()
   {
      GameManager.instance.NewGame();
   }

   public void Resume()
   {
      GameManager.instance.isPaused = false;
      GameManager.instance.OnGamePause();
   }

   public void Quit()
   {
      System.Diagnostics.Process.GetCurrentProcess().Kill(); //unity method for closing applications (Application.Quit), does not work for some reason.
   }

   private void CleanupSingletons()
   {
      Destroy(GameManager.instance.gameObject);
      Destroy(AudioManager.instance.gameObject);
   }
   
}
