using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerInitializer : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            var gameManager = Instantiate(new GameObject(), Vector2.zero, Quaternion.identity);
            gameManager.AddComponent<GameManager>();
        }
        if (AudioManager.Instance == null)
        {
            var audioManager = Instantiate(new GameObject(), Vector2.zero, Quaternion.identity);
            audioManager.AddComponent<AudioManager>();
        }
    }
}
