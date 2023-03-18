using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
   public void LoadGame()
   {
      SceneManager.LoadScene((int)Level.MainGame);
      //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
   }
}
