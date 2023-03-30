using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
   [SerializeField] private AudioClip _scoreAudio, _bounceAudio, _winAudio;
   private AudioSource _audioSource;
   public static AudioManager Instance;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         _audioSource = Instance.AddComponent<AudioSource>();
      }
      else Destroy(this); 
      
      DontDestroyOnLoad(this);
   }

   public void PlayScoreSound() => Instance._audioSource.PlayOneShot(_scoreAudio);
   public void PlayBounceSound() => Instance._audioSource.PlayOneShot(_bounceAudio);
   public void Win() => Instance._audioSource.PlayOneShot(_winAudio);
   public void ButtonHover() => Instance._audioSource.PlayOneShot(_bounceAudio);
}
