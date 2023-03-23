using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
   [SerializeField] AudioClip scoreAudio;
   [SerializeField] AudioClip bounceAudio;
   private AudioSource audioSource;
   public static AudioManager instance;

   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         audioSource = instance.AddComponent<AudioSource>();
      }
      else Destroy(this); 
      
      DontDestroyOnLoad(this);
   }

   public void Score()
   {
      instance.audioSource.PlayOneShot(scoreAudio);
   }

   public void Bounce()
   {
      instance.audioSource.PlayOneShot(bounceAudio);
   }
}
