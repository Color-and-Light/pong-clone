using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   private AudioClip scoreAudio;
   private AudioClip bounceWallAudio;
   private AudioClip bouncePaddleAudio;
   public static AudioManager instance;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else Destroy(this);
      
      DontDestroyOnLoad(this);
   }
}
