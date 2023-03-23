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
      SceneManager.LoadScene((int)Level.MainMenu);
      CleanupSingletons();

   }

   public void RestartGame()
   {
      CleanupSingletons();
      SceneManager.LoadScene((int)Level.MainGame);
   }

   public void Quit()
   {
      System.Diagnostics.Process.GetCurrentProcess().Kill(); //unity method for closing applications (Application.Quit), does not work for some reason.
   }

   private void CleanupSingletons()
   {
      Destroy(GameManager.instance);
      Destroy(AudioManager.instance);
   }
   
}
