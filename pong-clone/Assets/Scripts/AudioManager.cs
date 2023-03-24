using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioClip scoreAudio;
   [SerializeField] private AudioClip bounceAudio;
   [SerializeField] private AudioClip winAudio;
   
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

   public void PlayScoreSound()
   {
      instance.audioSource.PlayOneShot(scoreAudio);
   }

   public void PlayBounceSound()
   {
      instance.audioSource.PlayOneShot(bounceAudio);
   }

   public void Win()
   {
      instance.audioSource.PlayOneShot(winAudio);
   }

   public void ButtonHover()
   {
      instance.audioSource.PlayOneShot(bounceAudio);
   }
}
